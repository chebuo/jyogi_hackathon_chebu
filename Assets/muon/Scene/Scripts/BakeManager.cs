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

    private bool hasColorTimerStarted = false; 
    [Tooltip("色が変化し始めてからゲーム終了までの時間（この時間でタイマーが表示されます）")]
    public float finishTimerDuration = 3f; 

    [Header("■ 仕上げタイマー開始条件")]
    [Tooltip("この焼き加減の進行度を超えると、仕上げタイマーが開始可能な状態になります。（火から離れたら開始）")]
    [Range(0.0f, 1.0f)]
    public float startFinishTimerThreshold = 0.5f;

    private bool canStartFinishTimer = false;
    private bool isCurrentlyContactingFire = false; 
    private bool wasContactingFire = false; // 前フレームの接触状態を保持する

    private GameManager gameManager;

    private List<FireController> activeFires = new List<FireController>();

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseColor = spriteRenderer.color;
        
        gameManager = FindObjectOfType<GameManager>();

        if (gameOverText != null) {
            gameOverText.enabled = false;
        }
    }

    void Update() {
        if (gameManager != null && !gameManager.GetComponent<GameManager>().enabled)
        {
            return; 
        }

        FindActiveFires();
        FireController closestFire = GetClosestActiveFire();

        wasContactingFire = isCurrentlyContactingFire; // 現在の接触状態を次フレームのために保存
        isCurrentlyContactingFire = false; // このフレームの接触状態を初期化

        // --- 火の影響計算と接触判定 ---
        if (closestFire != null)
        {
            float distance = Vector2.Distance(transform.position, closestFire.transform.position);
            
            if (distance <= maxHeatDistance)
            {
                isCurrentlyContactingFire = true; // 火の範囲内にいる
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
        else // 火が一つも存在しない場合
        {
            isCurrentlyContactingFire = false;
        }
        // --- 修正ここまで ---

        // --- タイマー開始/停止ロジックの変更 ---
        // 焼き進捗が閾値を超えていて、かつ真っ黒焦げでなければタイマー開始可能状態
        if (bakeProgress >= startFinishTimerThreshold && !isBurntTooMuch)
        {
            canStartFinishTimer = true;
        }
        else
        {
            // 閾値に達していない、または真っ黒焦げになったらタイマー開始不可
            canStartFinishTimer = false;
        }

        // 接触状態が「接触」から「非接触」に変わった瞬間、かつタイマー開始可能状態であればタイマーを開始
        if (!isCurrentlyContactingFire && wasContactingFire && canStartFinishTimer)
        {
            Debug.Log("火から離れました！仕上げタイマーを開始します。");
            if (gameManager != null)
            {
                gameManager.StartInGameTimerDisplay(finishTimerDuration);
            }
        }
        // 接触状態が「非接触」から「接触」に変わった瞬間、または接触中の場合でタイマーが表示中であれば停止
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
        if (gameManager != null && !gameManager.GetComponent<GameManager>().enabled)
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
        else // タイマー終了によるゲーム終了の場合
        {
            isSuccess = finalScore >= 800;
            resultMessage = isSuccess ? "成功！" : "失敗...";
        }

        if (gameOverText != null) {
            gameOverText.text = $"ゲーム終了！最終スコア: {finalScore}\n{resultMessage}";
            gameOverText.enabled = true;
        }

        if (gameManager != null) {
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