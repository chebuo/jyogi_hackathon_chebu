using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

// テンプレート用のGameManager
// ゲームの開始、終了、UIの表示、シーン遷移などを管理します。
public class GameManager_01 : MonoBehaviour // クラス名をGameManager_01に変更
{
    [Header("■ カウントダウン設定")]
    [Tooltip("ゲーム開始時と、ゲーム内の特定のイベント（例：完成判定後）でカウントダウンを表示するText UIコンポーネントをここに設定します。")]
    public Text countdownText; // カウントダウン表示用のText UI
    [Tooltip("ゲーム開始時のカウントダウンの時間（秒）を設定します。")]
    public float initialCountdownDuration = 3f; // ゲーム開始時のカウントダウン時間

    [Header("■ ゲームプレイ制御オブジェクト")]
    [Tooltip("ゲームプレイを管理するオブジェクトをここに設定します。ゲーム開始/終了時に有効/無効を切り替えます。")]
    public GameObject gameplayRootObject; // ゲームプレイのルートとなるオブジェクト（例：今回のimoなど、ゲーム固有の要素をまとめた親オブジェクト）
    [Tooltip("imoオブジェクトにアタッチされているBakeManagerスクリプトをここに設定します。")]
    public BakeManager bakeManager; 
    [Tooltip("imoオブジェクトにアタッチされているImoDragスクリプトをここに設定します。")]
    public ImoDrag imoDrag; // ImoDragへの参照

    [Header("■ ゲーム終了UI設定")]
    [Tooltip("ゲーム終了時に表示されるパネル（例：GameOverPanel）をここに設定します。")]
    public GameObject gameOverPanel; // ゲーム終了パネル
    [Tooltip("リトライボタンをここに設定します。")]
    public Button retryButton; // リトライボタン
    [Tooltip("次のステージへ進むボタンをここに設定します。")]
    public Button nextStageButton; // 次のステージボタン

    private bool gameStarted = false; // ゲームが開始しているかどうかのフラグ
    private bool isTimerDisplaying = false; // タイマーが表示されているかどうかのフラグ

    void Start()
    {
        // ゲーム開始時にゲームプレイオブジェクトと関連スクリプトを一時的に無効化
        if (gameplayRootObject != null)
        {
            gameplayRootObject.SetActive(false);
        }
        if (bakeManager != null) { bakeManager.enabled = false; }
        if (imoDrag != null) { imoDrag.enabled = false; }

        // ゲーム終了UIは最初から非表示にする
        if (gameOverPanel != null) { gameOverPanel.SetActive(false); }
        if (retryButton != null) { retryButton.gameObject.SetActive(false); }
        if (nextStageButton != null) { nextStageButton.gameObject.SetActive(false); }
        
        // カウントダウンテキストは最初から非表示
        if (countdownText != null) { countdownText.gameObject.SetActive(false); } 

        // ゲーム開始時のカウントダウンを開始
        StartCoroutine(StartInitialCountdownRoutine());
    }

    // ゲーム開始時のカウントダウンコルーチン
    IEnumerator StartInitialCountdownRoutine()
    {
        float timer = initialCountdownDuration;
        if (countdownText != null) { countdownText.gameObject.SetActive(true); }

        while (timer > 0)
        {
            if (countdownText != null) { countdownText.text = Mathf.CeilToInt(timer).ToString(); }
            yield return new WaitForSeconds(1f);
            timer--;
        }

        if (countdownText != null) { countdownText.text = "スタート！"; }
        yield return new WaitForSeconds(1f);

        if (countdownText != null) { countdownText.gameObject.SetActive(false); }
        StartGame(); // カウントダウン終了後、ゲームを開始
    }

    // ゲーム開始処理
    void StartGame()
    {
        gameStarted = true;
        Debug.Log("ゲーム開始！");

        // ゲームプレイオブジェクトと関連スクリプトを有効化
        if (gameplayRootObject != null)
        {
            gameplayRootObject.SetActive(true);
        }
        if (bakeManager != null) { bakeManager.enabled = true; }
        if (imoDrag != null) { imoDrag.enabled = true; }

        // ゲーム終了UIは念のためここで非表示を徹底
        if (gameOverPanel != null) { gameOverPanel.SetActive(false); }
    }

    // ゲーム中の特定のイベント（例：焼き加減が完了に近づいた時）で呼び出されるタイマー表示開始メソッド
    // duration: タイマーの秒数
    public void StartInGameTimerDisplay(float duration)
    {
        if (countdownText != null)
        {
            StopAllCoroutines(); // 現在実行中のすべてのコルーチンを停止
            StartCoroutine(DisplayInGameTimerRoutine(duration));
            isTimerDisplaying = true; // タイマー表示中フラグをtrueに
        }
    }

    // タイマー表示を停止するメソッド
    public void StopInGameTimerDisplay()
    {
        if (countdownText != null)
        {
            StopAllCoroutines(); // タイマーのコルーチンを停止
            countdownText.gameObject.SetActive(false); // テキストを非表示にする
            isTimerDisplaying = false; // タイマー表示中フラグをfalseに
        }
    }

    // タイマーが表示中であるかを確認するメソッド
    public bool IsTimerDisplaying()
    {
        return isTimerDisplaying;
    }

    IEnumerator DisplayInGameTimerRoutine(float duration)
    {
        countdownText.gameObject.SetActive(true);
        float timer = duration;

        while (timer > 0)
        {
            countdownText.text = Mathf.CeilToInt(timer).ToString();
            yield return new WaitForSeconds(1f);
            timer--;
        }
        countdownText.gameObject.SetActive(false);
        isTimerDisplaying = false; // タイマーが0になったらフラグをfalseに
        
        // タイマーが0になったらゲームを終了する
        if (bakeManager != null)
        {
            bakeManager.ForceEndGameByTimer();
        }
    }

    // 外部（例：ゲーム固有のロジック）から呼び出されるゲーム終了メソッド
    // success: ゲームが成功したかどうか
    public void EndGame(bool success)
    {
        gameStarted = false; // ゲーム状態を終了に設定
        Debug.Log($"ゲーム終了！成功: {success}");

        // ゲームプレイオブジェクトと関連スクリプトを無効化
        if (gameplayRootObject != null)
        {
            gameplayRootObject.SetActive(false);
        }
        if (bakeManager != null) { bakeManager.enabled = false; }
        if (imoDrag != null) { imoDrag.enabled = false; }

        // カウントダウンテキストも非表示にする
        if (countdownText != null) { countdownText.gameObject.SetActive(false); }
        isTimerDisplaying = false; // ゲーム終了時もタイマーフラグをリセット

        // ゲーム終了UIを表示
        if (gameOverPanel != null) { gameOverPanel.SetActive(true); }
        if (retryButton != null) { retryButton.gameObject.SetActive(true); }

        if (success)
        {
            if (nextStageButton != null) { nextStageButton.gameObject.SetActive(true); }
            Debug.Log("ゲーム成功！次のステージへ進めます。");
        }
        else
        {
            if (nextStageButton != null) { nextStageButton.gameObject.SetActive(false); }
            Debug.Log("ゲーム失敗...リトライしてください。");
        }
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
        SceneManager.LoadScene("Title");
    }
}