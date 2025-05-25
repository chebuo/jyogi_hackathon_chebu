using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BakeManager : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private Color baseColor;

    [Header("■ 焼き加減と火の設定")]
    [Tooltip("熱がimoに影響する最大の距離")]
    public float maxHeatDistance = 2.0f;
    [Tooltip("焼き加減が進む速さの基本値")]
    public float baseHeatSpeed = 0.5f;
    [Tooltip("火の大きさによる焼き速度への影響度")]
    public float scaleInfluenceFactor = 0.2f;

    private float bakeProgress = 0f;
    
    [Header("■ 真っ黒焦げ判定設定")]
    [Tooltip("この焼き加減の進行度を超えると、真っ黒焦げで即ゲームオーバーになります。")]
    [Range(0.0f, 1.0f)]
    public float burntThreshold = 0.9f;
    private bool isBurntTooMuch = false;

    // UI設定
    [Header("UI設定")]
    public Text gameOverText;
    public Color idealColor;

    [Tooltip("火から離れてからゲーム終了までの時間（この時間でタイマーが表示されます）")]
    public float finishTimerDuration = 3f; 
    
    [Header("■ 仕上げタイマー開始条件")]
    [Tooltip("この焼き加減の進行度を超えると、仕上げタイマーが開始可能な状態になります。（火から離れたら開始）")]
    [Range(0.0f, 1.0f)]
    public float startFinishTimerThreshold = 0.5f;

    private bool canStartFinishTimer = false;
    private bool isCurrentlyContactingFire = false; 
    private bool wasContactingFire = false;

    private GameManager_01 gameManager; // GameManager_01を参照するように変更

    private List<FireController> activeFires = new List<FireController>();

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseColor = spriteRenderer.color;
        
        gameManager = FindObjectOfType<GameManager_01>(); // GameManager_01を参照するように変更

        if (gameOverText != null) {
            gameOverText.enabled = false;
        }
    }

    void Update() {
        // gameManagerがnullでないか、かつenabledになっているかを確認
        // gameManager.GetComponent<GameManager_01>().enabled; を使う場合は、gameManagerがnullでないか最初に確認
        if (gameManager == null || !gameManager.enabled) // GameManager_01のenabledで確認
        {
            return; 
        }

        FindActiveFires();
        FireController closestFire = GetClosestActiveFire();

        wasContactingFire = isCurrentlyContactingFire; 
        isCurrentlyContactingFire = false; 

        if (closestFire != null)
        {
            float distance = Vector2.Distance(transform.position, closestFire.transform.position);
            
            if (distance <= maxHeatDistance)
            {
                isCurrentlyContactingFire = true; 
            }

            float fireScaleY = closestFire.GetCurrentScale().y;
            float scaleAdjustedHeatSpeed = baseHeatSpeed * (1f + fireScaleY * scaleInfluenceFactor);
            float heatFactor = Mathf.Clamp01(1f - (distance / maxHeatDistance));
            
            bakeProgress = Mathf.Clamp(bakeProgress + (heatFactor * scaleAdjustedHeatSpeed * Time.deltaTime), 0f, 1f);

            SetBakeProgress(bakeProgress);

            if (bakeProgress >= burntThreshold && !isBurntTooMuch)
            {
                isBurntTooMuch = true;
                EndGame();
                return; 
            }
        }
        else 
        {
            isCurrentlyContactingFire = false;
        }

        if (bakeProgress >= startFinishTimerThreshold && !isBurntTooMuch)
        {
            canStartFinishTimer = true;
        }
        else
        {
            canStartFinishTimer = false;
        }

        if (!isCurrentlyContactingFire && wasContactingFire && canStartFinishTimer)
        {
            Debug.Log("火から離れました！仕上げタイマーを開始します。");
            if (gameManager != null)
            {
                gameManager.StartInGameTimerDisplay(finishTimerDuration);
            }
        }
        // ここでgameManagerがnullでないか、かつgameManager.IsTimerDisplaying() を呼び出す前にgameManagerが有効かチェック
        else if (isCurrentlyContactingFire && gameManager != null && gameManager.IsTimerDisplaying())
        {
             Debug.Log("火に接触しました。仕上げタイマーを停止。");
             gameManager.StopInGameTimerDisplay();
        }
    }

    void FindActiveFires()
    {
        activeFires.Clear();
        FireController[] allFiresInScene = FindObjectsOfType<FireController>();
        foreach (FireController fire in allFiresInScene)
        {
            if (fire.gameObject.activeInHierarchy)
            {
                activeFires.Add(fire);
            }
        }
    }

    FireController GetClosestActiveFire()
    {
        FireController closestFire = null;
        float minDistance = Mathf.Infinity;

        foreach (FireController fire in activeFires)
        {
            float distance = Vector2.Distance(transform.position, fire.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestFire = fire;
            }
        }
        return closestFire;
    }

    public void SetBakeProgress(float progress) {
        float t = Mathf.Clamp01(progress);
        Color newColor = baseColor * (1f - t);
        newColor.a = baseColor.a;

        spriteRenderer.color = newColor;
    }

    public void ForceEndGameByTimer()
    {
        if (!isBurntTooMuch) 
        {
            EndGame();
        }
    }

    private void EndGame() {
        // gameManagerがnullでないか、かつenabledになっているかを確認
        if (gameManager == null || !gameManager.enabled)
        {
            return;
        }

        int finalScore = GetBakeScore();
        string resultMessage;
        bool isSuccess = false;

        if (isBurntTooMuch)
        {
            resultMessage = "真っ黒焦げ！";
            finalScore = 0;
            isSuccess = false;
        }
        else 
        {
            isSuccess = finalScore >= 800;
            resultMessage = isSuccess ? "成功！" : "失敗...";
        }

        if (gameOverText != null) {
            gameOverText.text = $"ゲーム終了！最終スコア: {finalScore}\n{resultMessage}";
            gameOverText.enabled = true;
        }

        if (gameManager != null) { // gameManagerがnullチェック済みだが、念のため
            gameManager.EndGame(isSuccess);
            this.enabled = false; 
        }
    }

    public int GetBakeScore() {
        Color currentColor = spriteRenderer.color;
        float distance = Mathf.Sqrt(
            Mathf.Pow(currentColor.r - idealColor.r, 2) +
            Mathf.Pow(currentColor.g - idealColor.g, 2) +
            Mathf.Pow(currentColor.b - idealColor.b, 2)
        );
        return Mathf.FloorToInt(Mathf.Clamp(1000 * (1f - distance), 0, 1000));
    }
}