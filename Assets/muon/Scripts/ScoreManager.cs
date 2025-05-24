using UnityEngine;
using UnityEngine.UI;

// 汎用的なスコア管理スクリプト
// UIテキストにスコアを表示します。
public class ScoreManager : MonoBehaviour {
    [Tooltip("スコアを表示するためのText UIコンポーネントをここに設定します。")]
    public Text scoreText; // スコア表示用のText UI
    private int score = 0; // 現在のスコア

    void Start() {
        UpdateScoreText(); // ゲーム開始時にスコアを初期表示
    }

    // スコアを加算するメソッド
    // amount: 加算するスコアの量
    public void AddScore(int amount) {
        score += amount;
        UpdateScoreText(); // スコア更新後に表示を更新
    }

    // 現在のスコアを取得するメソッド
    public int GetCurrentScore()
    {
        return score;
    }

    // スコアをリセットするメソッド
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    // スコアの表示を更新する内部メソッド
    private void UpdateScoreText() {
        if (scoreText != null)
        {
            scoreText.text = "スコア: " + score;
        }
        else
        {
            Debug.LogWarning("ScoreManager: ScoreTextが設定されていません！");
        }
    }
}