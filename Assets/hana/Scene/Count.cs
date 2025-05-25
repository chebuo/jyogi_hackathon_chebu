using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Count : MonoBehaviour
{

    public static Count instance;
    private int totalClickCount = 0;
    public Text totalClickText;
    CountdownTimer countdown;
    private int Score = 0;
    public Text ScoreText;
    public Text Jage;
   
   
    void Awake()
    {
        countdown = GetComponent<CountdownTimer>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateUI();  // 初期表示
        //if (ScoreText != null) { nextStageButton.gameObject.SetActive(false); }

    }

    public void AddClick()
    {
        if (countdown.currentTime>0)
        {
            totalClickCount++;
            Debug.Log($"クリック数: {totalClickCount}"); // デバッグ表示

            Score = totalClickCount * 20;
            Debug.Log($"スコア:{Score}");//デバッグ表示
            UpdateUI();
        }


    }
    private void Update()
    {
        Debug.Log(countdown.currentTime);
    }
    void UpdateUI()
    {
       
        if (totalClickText != null)
        {
            totalClickText.text = $"Click:{totalClickCount}"; // テキストの更新
            ScoreText.text = (($"Score:{Score}"));


        }
        else
        {
            Debug.LogWarning("totalClickText が設定されていません！");
        }


        

        if (countdown.currentTime ==0)
        {
            if (Score< 400)
            {
                Jage.text = ("消火失敗");
            }
            else 
            {         
                Jage.text = ("消火成功");
            }
        }


    }





   
}