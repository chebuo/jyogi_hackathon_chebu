using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public Text scoreText;
    private int score = 0;

    void Start() {
        UpdateScoreText();
    }

    public void AddScore(int amount) {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText() {
        scoreText.text = "ÉXÉRÉA: " + score;
    }
}