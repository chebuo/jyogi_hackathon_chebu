using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [Header("UI設定")]
    public Text gamescoretext;

    int GetScore = 1000;

    private void Start()
    {
        Score();
    }

    private void Score()
    {
        int finalscore = GetScore;
        string resultMessage = finalscore >= 1200 ? "成功!" : "失敗…";

        if (gamescoretext != null)
        {
            gamescoretext.text = $"ゲーム終了!最終スコア:{finalscore}\n{resultMessage}";
        }
    }
}