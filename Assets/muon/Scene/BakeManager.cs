using UnityEngine;
using UnityEngine.UI;

public class BakeManager : MonoBehaviour {
    // BakeManager自身の情報（imoオブジェクトにアタッチされている想定）
    private SpriteRenderer spriteRenderer;
    private Color baseColor; // imoオブジェクトの初期色

    // --- ここから端的なコメント ---
    [Header("■ 焼き加減と火の設定")] // UnityのInspectorで表示されるヘッダー

    [Tooltip("火のオブジェクトのTransform")] // Inspectorでの説明
    public Transform fireTransform; // 火の場所となるオブジェクト

    [Tooltip("熱がimoに影響する最大の距離")]
    public float maxHeatDistance = 2.0f; // 熱が届く最大距離

    [Tooltip("焼き加減が進む速さ")]
    public float heatSpeed = 0.5f; // 焼きの進行速度

    private float bakeProgress = 0f; // 現在の焼きの進行度（0.0～1.0）
    // --- ここまで ---

    [Header("UI設定")] // UnityのInspectorで表示されるヘッダー
    public Text gameOverText; // ゲーム終了メッセージ表示用Text UI
    public Color idealColor; // 理想の焼き色

    private bool hasColorChanged = false; // 色が一度でも変化したか
    private float lastColorChangeTime = 0f; // 色の最終変化時間
    private float checkDelay = 3f; // ゲーム終了判定までの遅延時間

    private GameManager gameManager; // ゲーム全体の管理スクリプト

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseColor = spriteRenderer.color;
        lastColorChangeTime = Time.time;

        gameManager = FindObjectOfType<GameManager>();

        if (gameOverText != null) {
            gameOverText.enabled = false; // ゲーム開始時は非表示
        }

        if (fireTransform == null) {
            Debug.LogError("BakeManager: '火の場所 (Fire Transform)' が設定されていません！InspectorでFireZoneをD&Dしてください。");
        }
    }

    void Update() {
        if (fireTransform != null) {
            float distance = Vector2.Distance(transform.position, fireTransform.position);
            float heatFactor = Mathf.Clamp01(1f - (distance / maxHeatDistance));
            bakeProgress = Mathf.Clamp(bakeProgress + (heatFactor * heatSpeed * Time.deltaTime), 0f, 1f);

            SetBakeProgress(bakeProgress);
        }

        if (hasColorChanged && Time.time - lastColorChangeTime >= checkDelay) {
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
        int finalScore = GetBakeScore();
        string resultMessage = finalScore >= 800 ? "成功！" : "失敗...";

        if (gameOverText != null) {
            gameOverText.text = $"ゲーム終了！最終スコア: {finalScore}\n{resultMessage}";
            gameOverText.enabled = true;
        }

        if (gameManager != null) {
            gameManager.EndGame(finalScore >= 800);
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