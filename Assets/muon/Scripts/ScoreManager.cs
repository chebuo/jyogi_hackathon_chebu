using UnityEngine;
// using UnityEngine.UI; // Textを使わないので不要になる

public class ScoreManager : MonoBehaviour {
    // [Tooltip("スコアを表示するためのText UIコンポーネントをここに設定します。")] // 削除
    // public Text scoreText; // 削除
    private int score = 0; // 現在のスコア

    void Start() {
        // UpdateScoreText(); // 削除
    }

    public void AddScore(int amount) {
        score += amount;
        // UpdateScoreText(); // 削除
    }

    public int GetCurrentScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
        // UpdateScoreText(); // 削除
    }

    // private void UpdateScoreText() { // 削除
    //     if (scoreText != null)
    //     {
    //         scoreText.text = "スコア: " + score;
    //     }
    //     else
    //     {
    //         Debug.LogWarning("ScoreManager: ScoreTextが設定されていません！");
    //     }
    // } // 削除
}