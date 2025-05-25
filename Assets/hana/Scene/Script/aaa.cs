using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using JetBrains.Annotations; // シーン管理のために必要

// テンプレート用のGameManager
// ゲームの開始、終了、UIの表示、シーン遷移などを管理します。
public class aaa : MonoBehaviour
{
    [Header("■ カウントダウン設定")]
    [Tooltip("ゲーム開始時と、ゲーム内の特定のイベント（例：完成判定後）でカウントダウンを表示するText UIコンポーネントをここに設定します。")]
    public Text countdownText; // カウントダウン表示用のText UI
    [Tooltip("ゲーム開始時のカウントダウンの時間（秒）を設定します。")]
    public float initialCountdownDuration = 3f; // ゲーム開始時のカウントダウン時間

    [Header("■ ゲームプレイ制御オブジェクト")]
    [Tooltip("ゲームプレイを管理するオブジェクトをここに設定します。ゲーム開始/終了時に有効/無効を切り替えます。")]
    public GameObject gameplayRootObject; // ゲームプレイのルートとなるオブジェクト（例：今回のimoなど、ゲーム固有の要素をまとめた親オブジェクト）

    [Header("■ ゲーム終了UI設定")]
    [Tooltip("ゲーム終了時に表示されるパネル（例：GameOverPanel）をここに設定します。")]
    public GameObject gameOverPanel; // ゲーム終了パネル
    [Tooltip("リトライボタンをここに設定します。")]
    public Button retryButton; // リトライボタン
    [Tooltip("次のステージへ進むボタンをここに設定します。")]
    public Button nextStageButton; // 次のステージボタン

    private bool gameStarted = false; // ゲームが開始しているかどうかのフラグ

    //public Button StartButton;//スタートボタン

    void Start()
    {
        // ゲーム開始時にゲームプレイオブジェクトを一時的に無効化
        // 外部から設定されなかった場合に備えてnullチェック
        if (gameplayRootObject != null)
        {
            gameplayRootObject.SetActive(false);
        }

        // ゲーム終了UIは最初から非表示にする
        if (gameOverPanel != null) { gameOverPanel.SetActive(false); }
        if (retryButton != null) { retryButton.gameObject.SetActive(false); }
        if (nextStageButton != null) { nextStageButton.gameObject.SetActive(false); }

        // カウントダウンテキストは最初から非表示
        if (countdownText != null) { countdownText.gameObject.SetActive(false); }

        // ゲーム開始時のカウントダウンを開始
        StartCoroutine(StartInitialCountdownRoutine());


        //if (StartButton != null) { StartButton.gameObject.SetActive(true); }
        //if (StartButton == null) { StartButton.gameObject.SetActive(true); }

        
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
    {                gameStarted = true;
        Debug.Log("ゲーム開始！");

        // ゲームプレイオブジェクトを有効化
        if (gameplayRootObject != null)
        {
            gameplayRootObject.SetActive(true);

            // ここでゲーム固有のロジック（例：プレイヤー操作スクリプトの有効化など）を呼び出す
            // 例えば、ImoDragやBakeManagerのenabledをtrueにする処理は、
            // gameplayRootObjectが有効化されるときに、その子スクリプトが自動的にStart()を呼ばれるようにするか、
            // もしくはGameManagerでそれらのスクリプトを参照してenabledを切り替えるようにする。
            // 汎用性のため、ここでは直接参照せずに、gameplayRootObjectがそれらを管理する前提とする。
        }

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
            StartCoroutine(DisplayInGameTimerRoutine(duration)); // 新しいタイマーコルーチンを開始
        }
    }

   


    // ゲーム中のタイマー表示コルーチン
    IEnumerator DisplayInGameTimerRoutine(float duration)
    {
        countdownText.gameObject.SetActive(true); // カウントダウンテキストを表示
        float timer = duration;

        while (timer > 0)
        {
            countdownText.text = Mathf.CeilToInt(timer).ToString(); // 残り秒数を表示
            yield return new WaitForSeconds(1f);
            timer--;
        }
        countdownText.gameObject.SetActive(false); // タイマーが0になったら非表示

        // タイマーが0になったらゲームを終了する（具体的なゲーム終了ロジックはテンプレート利用側で定義）
        Debug.Log("ゲーム内タイマーが終了しました。ゲーム終了処理を呼び出します。");
        // ここでゲーム終了（成功/失敗）の判定を行い、EndGameを呼び出す
        // 例: BakeManagerのForceEndGameByTimer()のようなメソッドを呼び出す想定
        // 汎用性を考慮し、ここでは直接EndGameを呼び出さず、外部からのトリガーを待つ形にする
        // ただし、最もシンプルな形として、直接EndGameを呼び出すことも可能
        // EndGame(false); // 例えばタイマー終了は失敗として扱うなど
    }

    // ゲーム中のタイマー表示を停止するメソッド（例：火から離れた時）
    public void StopInGameTimerDisplay()
    {
        if (countdownText != null)
        {
            StopAllCoroutines(); // タイマーのコルーチンを停止
            countdownText.gameObject.SetActive(false); // テキストを非表示にする
        }

    }


    

    // 外部（例：ゲーム固有のロジック）から呼び出されるゲーム終了メソッド
    // success: ゲームが成功したかどうか
    public void EndGame(bool success)
    {

        if (countdownText != null) { countdownText.gameObject.SetActive(false); }


        gameStarted = false; // ゲーム状態を終了に設定
        Debug.Log($"ゲーム終了！成功: {success}");

       

        // ゲームプレイオブジェクトを無効化
        if (gameplayRootObject != null)
        {
            gameplayRootObject.SetActive(false);
            // ここでゲーム固有のロジック（例：プレイヤー操作スクリプトの無効化など）を呼び出す
        }

        // カウントダウンテキストも非表示にする
        if (countdownText != null) { countdownText.gameObject.SetActive(false); }

        // ゲーム終了UIを表示
        if (gameOverPanel != null) { gameOverPanel.SetActive(true); }
        if (retryButton != null) { retryButton.gameObject.SetActive(true); } // リトライボタンは常に表示

        if (success)
        {
            if (nextStageButton != null) { nextStageButton.gameObject.SetActive(true); } // 成功の場合のみ次のステージボタンを表示
            Debug.Log("ゲーム成功！次のステージへ進めます。");
        }
        else
        {
            if (nextStageButton != null) { nextStageButton.gameObject.SetActive(false); } // 失敗の場合、次のステージボタンは非表示
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
        SceneManager.LoadScene("chebuo");
    }
}