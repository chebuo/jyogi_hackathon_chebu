using UnityEngine;
using UnityEngine.UI;

public class BakeManager : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private Color baseColor; // imoオブジェクトの初期色

    [Header("■ 焼き加減と火の設定")]
    [Tooltip("火のオブジェクトのTransform")]
    public Transform fireTransform;
    [Tooltip("熱がimoに影響する最大の最大の距離")]
    public float maxHeatDistance = 2.0f;
    [Tooltip("焼き加減が進む速さ")]
    public float heatSpeed = 0.5f;

    private float bakeProgress = 0f; // 現在の焼きの進行度（0.0～1.0）
    
    [Header("■ 真っ黒焦げ判定設定")]
    [Tooltip("この焼き加減の進行度を超えると、真っ黒焦げで即ゲームオーバーになります。")]
    [Range(0.0f, 1.0f)]
    public float burntThreshold = 0.9f;
    private bool isBurntTooMuch = false; // 真っ黒焦げになったかどうかのフラグ

    [Header("UI設定")]
    public Text gameOverText;
    public Color idealColor;

    private bool hasColorChanged = false;
    private float lastColorChangeTime = 0f;
    private float checkDelay = 3f;

    private GameManager gameManager;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseColor = spriteRenderer.color;
        lastColorChangeTime = Time.time;

        gameManager = FindObjectOfType<GameManager>();

        if (gameOverText != null) {
            gameOverText.enabled = false;
        }

        if (fireTransform == null)
        {
            Debug.LogError("BakeManager: '火の場所 (Fire Transform)' が設定されていません！InspectorでFireZoneをD&Dしてください。");
        }
    }

    void Update() {
        if (gameManager != null && !gameManager.GetComponent<GameManager>().enabled)
        {
            return; 
        }

        if (fireTransform != null)
        {
            float distance = Vector2.Distance(transform.position, fireTransform.position);
            float heatFactor = Mathf.Clamp01(1f - (distance / maxHeatDistance));
            bakeProgress = Mathf.Clamp(bakeProgress + (heatFactor * heatSpeed * Time.deltaTime), 0f, 1f);

            SetBakeProgress(bakeProgress);

            if (bakeProgress >= burntThreshold && !isBurntTooMuch)
            {
                isBurntTooMuch = true;
                EndGame(); // 即座にゲームオーバーを呼び出す
            }
        }
        
        // 通常のゲーム終了判定（真っ黒焦げで終了しない場合）
        // 真っ黒焦げになったら、この条件ではEndGameを呼ばない
        if (hasColorChanged && Time.time - lastColorChangeTime >= checkDelay && !isBurntTooMuch) {
            EndGame();
        }
    }

    public void SetBakeProgress(float progress) {
        float t = Mathf.Clamp01(progress);
        Color newColor = baseColor * (1f - t);
        newColor.a = baseColor.a;

        if (spriteRenderer.color != newColor) {
            hasColorChanged = true;
            lastColorChangeTime = Time.time;
        }
        spriteRenderer.color = newColor;
    }

    private void EndGame() {
        // すでにゲームオーバー状態でないかチェック（多重呼び出し防止）
        if (gameManager != null && !gameManager.GetComponent<GameManager>().enabled)
        {
            return;
        }

        int finalScore = GetBakeScore();
        string resultMessage;
        bool isSuccess = false; // GameManagerに渡す成否判定

        if (isBurntTooMuch) // 真っ黒焦げの場合
        {
            resultMessage = "真っ黒焦げ！"; // 「真っ黒焦げ！」というメッセージに
            finalScore = 0; // スコアは0にする
            isSuccess = false; // 失敗扱い
        }
        else // 通常の終了判定の場合
        {
            isSuccess = finalScore >= 800; // スコアに基づいて成功か失敗かを判定
            resultMessage = isSuccess ? "成功！" : "失敗...";
        }

        if (gameOverText != null) {
            gameOverText.text = $"ゲーム終了！最終スコア: {finalScore}\n{resultMessage}";
            gameOverText.enabled = true;
        }

        if (gameManager != null) {
            gameManager.EndGame(isSuccess); // GameManagerに最終的な成否を渡す
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