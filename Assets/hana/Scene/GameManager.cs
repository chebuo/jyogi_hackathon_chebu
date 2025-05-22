using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // 他スクリプトから使えるように
    private int totalClickCount = 0;

    public Text totalClickText;  // UI Text（Inspector で設定）

    void Awake()
    {
        instance = this;
    }

    public void AddClick()
    {
        totalClickCount++;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (totalClickText != null)
        {
            totalClickText.text = "Click: " + totalClickCount;
        }
    }
}
