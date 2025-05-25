using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GAME : MonoBehaviour
{
    [Header("ゲーム終了UI")]
    [Tooltip("ゲーム終了時に表示されるパネル設定")]
    public GameObject gameOverPanel; // ゲーム終了パネル
    [Tooltip("リトライボタン設定")]
    public Button retryButton; // リトライボタン
    [Tooltip("次のステージへボタン設定")]
    public Button nextStageButton; // 次のステージボタン
    [Tooltip("スコアテキスト")]
    public GameObject scoretext; // 次のステージボタン


    private int hitCount = 0;
    private int missCount = 0;
    private const int maxCount = 30; // 合計30回でゲーム終了
    float time = 0;

    public static GAME Instance; // シングルトンパターンでアクセス可能に

    void Start()
    {
        // ゲーム終了UIは最初から非表示にする
        if (gameOverPanel != null) { gameOverPanel.SetActive(false); }
        if (retryButton != null) { retryButton.gameObject.SetActive(false); }
        if (nextStageButton != null) { nextStageButton.gameObject.SetActive(false); }
        if (scoretext != null) { scoretext.SetActive(false); }

    }
    void startGame()
    {
        if (gameOverPanel != null) { gameOverPanel.SetActive(false); }
        if (scoretext != null) { scoretext.SetActive(false); }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;//成功
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterHit()
    {
        hitCount++;
        CheckGameEnd();//成功
    }

    public void RegisterMiss()
    {
        missCount++;
        CheckGameEnd();//成功
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 40f)
        {
            EndGame();//dekita!!!!!
            Debug.Log("dekiteru");
        }
    }

     void CheckGameEnd()
    {
        int total = hitCount + missCount;
        if(total==15)
        {
            EndGame();//dekita!!!!!
            Debug.Log("dekiteru");
        }
    }

  

    private void EndGame()
    {
        Debug.Log("ゲームを終了します");//dekita   but ちょい早い

        // ゲーム終了UIを表示
        if (gameOverPanel != null) { gameOverPanel.SetActive(true); }
        if (retryButton != null) { retryButton.gameObject.SetActive(true); } // リトライボタンは常に表示
        if (nextStageButton != null) { nextStageButton.gameObject.SetActive(true); }
        if (scoretext != null) { scoretext.SetActive(true); }
    }

    // ゲームのリトライ処理（ボタンから呼び出す）
    public void RestartGame()
    {
        Debug.Log("ゲームをリトライします...");
        // 現在のシーンを再読み込みしてゲームをリセット
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 次のステージへ進む処理（ボタンから呼び出す）
    public void GoToNextStage()
    {
        Debug.Log("次のステージへ進みます...");
        // 実際のゲームでは、次のシーン名（例: "Stage2"）を指定
        // 例: SceneManager.LoadScene("Stage2");
        // ここでは、現在のシーンを再読み込みするダミー処理
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}