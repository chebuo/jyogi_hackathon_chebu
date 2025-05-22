using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // ���X�N���v�g����g����悤��
    private int totalClickCount = 0;

    public Text totalClickText;  // UI Text�iInspector �Őݒ�j

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
