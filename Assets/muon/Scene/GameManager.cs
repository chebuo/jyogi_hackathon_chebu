using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; // シーン管理のために必要

public class GameManager : MonoBehaviour
{
    [Header("カウントダウン設定")]
    [Tooltip("カウントダウンを表示するText UIコンポーネントをここに設定します。")]
    public Text countdownText;
    [Tooltip("カウントダウンの時間（秒）を設定します。")]
    public float countdownDuration = 3f;

    [Header("ゲームオブジェクト参照")]
    [Tooltip("操作するimoオブジェクトをここに設定します。")]
    public GameObject imoObject;
    [Tooltip("imoオブジェクトにアタッチされているBakeManagerスクリプトをここに設定します。")]
    public BakeManager bakeManager;
    [Tooltip("imoオブジェクトにアタッチされているImoDragスクリプトをここに設定します。")]
    public ImoDrag imoDrag;

    [Header("ゲーム終了UI設定")] // 新しいヘッダーを追加
    [Tooltip("ゲーム終了時に表示されるパネル（GameOverPanel）をここに設定します。")]
    public GameObject gameOverPanel; // GameOverPanelへの参照
    [Tooltip("リトライボタンをここに設定します。")]
    public Button retryButton; // リトライボタンへの参照
    [Tooltip("次のステージへ進むボタンをここに設定します。")]
    public Button nextStageButton; // 次のステージボタンへの参照

    private bool gameStarted = false;

    void Start()
    {
        // ゲーム開始時にimoオブジェクトと関連スクリプトを一時的に無効化
        if (imoObject != null) { imoObject.SetActive(false); }
        if (bakeManager != null) { bakeManager.enabled = false; }
        if (imoDrag != null) { imoDrag.enabled = false; }

        // ゲーム終了UIは最初から非表示にする
        if (gameOverPanel != null) { gameOverPanel.SetActive(false); }
        if (retryButton != null) { retryButton.gameObject.SetActive(false); }
        if (nextStageButton != null) { nextStageButton.gameObject.SetActive(false); }


        // カウントダウンを開始
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        float timer = countdownDuration;
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
        StartGame();
    }

    void StartGame()
    {
        gameStarted = true;
        Debug.Log("ゲーム開始！");

        // imoオブジェクトと関連スクリプトを有効化
        if (imoObject != null) { imoObject.SetActive(true); }
        if (bakeManager != null) { bakeManager.enabled = true; }
        if (imoDrag != null) { imoDrag.enabled = true; }

        // ゲーム終了UIは念のためここで非表示を徹底
        if (gameOverPanel != null) { gameOverPanel.SetActive(false); }
    }

    // ゲーム終了時にBakeManagerから呼び出すメソッド
    public void EndGame(bool success)
    {
        gameStarted = false;
        Debug.Log($"ゲーム終了！成功: {success}");

        // imoの操作を無効化
        if (imoDrag != null) { imoDrag.enabled = false; }
        if (bakeManager != null) { bakeManager.enabled = false; } // 焼きの進行も停止

        // ゲーム終了UIを表示
        if (gameOverPanel != null) { gameOverPanel.SetActive(true); }
        if (retryButton != null) { retryButton.gameObject.SetActive(true); } // リトライボタンは常に表示

        if (success)
        {
            // 成功の場合、次のステージボタンも表示
            if (nextStageButton != null) { nextStageButton.gameObject.SetActive(true); }
            Debug.Log("ゲーム成功！次のステージへ進めます。");
        }
        else
        {
            // 失敗の場合、次のステージボタンは非表示を徹底
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
        // ここに次のシーンをロードするロジックを記述
        // 例: SceneManager.LoadScene("NextStageSceneName");
        // とりあえず今回はリトライと同じ動作にしておきます。
        // 実際のゲームでは、次のシーン名（"Stage2"など）を指定してください。
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}