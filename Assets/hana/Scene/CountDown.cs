using UnityEngine;
using UnityEngine.UI;
using System;
using JetBrains.Annotations; // Actionを使うために必要

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 10f; // カウントダウンの初期時間
    public float currentTime;
    public Text timerText; // UIに表示する場合
    public GameObject retyButton;//リトライボタン
    public GameObject nextButton;//ネクストボタン
    public GameObject gameOverPanel;//ゲームオーバーパネル
   

    // カウントダウンが0になったことを他のスクリプトに通知するためのイベント
    // staticにすることで、CountdownTimerのインスタンスを参照せずにイベントに登録できます
    public static event Action OnCountdownFinished;

    void Start()
    {
        currentTime = countdownTime;
        UpdateTimerDisplay(); // 初期表示
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();

            if (currentTime <= 0)
            {
                currentTime = 0;
                // カウントダウンが終了したらイベントを発火させる
                // ?.Invoke() は、イベントに登録されているメソッドがなければ何もしない安全な呼び出し方
                OnCountdownFinished?.Invoke();
                Debug.Log("カウントダウン終了！");
                this.retyButton.SetActive(true);
                this.nextButton.SetActive(true);
                this.gameOverPanel.SetActive(true);
               
            }
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + currentTime.ToString("F1");
        }
       
    }
}
