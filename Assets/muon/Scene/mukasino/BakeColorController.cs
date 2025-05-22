using UnityEngine;
using UnityEngine.UI; // Textを使うために必要

public class BakeColorController : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private Color baseColor;

    [Header("UI設定")]
    public Text gameOverText; // Text型であることを確認

    [Header("理想の焼き色")]
    public Color idealColor;

    private bool hasColorChanged = false;
    private float lastColorChangeTime = 0f;
    private float checkDelay = 3f;

    private GameManager gameManager; // GameManagerへの参照

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseColor = spriteRenderer.color;
        lastColorChangeTime = Time.time;

        // GameManagerの参照を取得
        gameManager = FindObjectOfType<GameManager>();

        // ゲーム開始時は非表示
        if (gameOverText != null) {
            gameOverText.enabled = false;
        }
    }

    void Update() {
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