using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [Header("UI�ݒ�")]
    public Text gamescoretext;

    int GetScore = 1000;

    private void Start()
    {
        Score();
    }

    private void Score()
    {
        int finalscore = GetScore;
        string resultMessage = finalscore >= 1200 ? "����!" : "���s�c";

        if (gamescoretext != null)
        {
            gamescoretext.text = $"�Q�[���I��!�ŏI�X�R�A:{finalscore}\n{resultMessage}";
        }
    }
}