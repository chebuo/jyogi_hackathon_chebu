using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager_01 : MonoBehaviour
{
    [Header("■ カウントダウン設定")]
    [Tooltip("ゲーム開始時と、ゲーム内の特定のイベント（例：完成判定後）でカウントダウンを表示するText UIコンポーネントをここに設定します。")]
    public Text countdownText;
    [Tooltip("ゲーム開始時のカウントダウンの時間（秒）を設定します。")]
    public float initialCountdownDuration = 3f;

    [Header("■ ゲームプレイ制御オブジェクト")]
    [Tooltip("プレイヤーが操作するimoオブジェクトをここに設定します。")]
    public GameObject imoObject;
    [Tooltip("ゲームプレイシーンの背景オブジェクトをここに設定します。")]
    public GameObject haikeiObject;
    [Tooltip("火を生成するFireManagerオブジェクトをここに設定します。")]
    public GameObject fireManagerObject;
    [Tooltip("FireManagerオブジェクトにアタッチされているFireSpawner_01スクリプトをここに設定します。")]
    public FireSpawner_01 fireSpawner_01;
    // --- 変更点: syuryoObject はGameplayではなくGameOverPanelの子になるため、ゲームプレイ中の管理からは外す ---
    // [Tooltip("スコア表示用UIオブジェクトをここに設定します。")] // この行を削除
    // public GameObject syuryoObject; // この行を削除
    // --- 変更点ここまで ---

    [Tooltip("imoオブジェクトにアタッチされているBakeManagerスクリプトをここに設定します。")]
    public BakeManager bakeManager;
    [Tooltip("imoオブジェクトにアタッチされているImoDragスクリプトをここに設定します。")]
    public ImoDrag imoDrag;

    [Tooltip("オンラインリーダーボードを管理するスクリプトをここに設定します。")]
    public OnlineScoreManager_01 onlineScoreManager_01;

    [Header("■ ゲーム終了UI設定")]
    [Tooltip("ゲーム終了時に表示されるパネル（例：GameOverPanel）をここに設定します。")]
    public GameObject gameOverPanel;
    [Tooltip("リトライボタンをここに設定します。")]
    public Button retryButton;
    [Tooltip("次のステージへ進むボタンをここに設定します。")]
    public Button nextStageButton;

    // --- 追加: GameOverPanel内のリーダーボード表示用Text UI ---
    [Tooltip("ゲーム終了パネル内のリーダーボード表示用Text UIをここに設定します。（syuryoオブジェクト）")]
    public Text gameOverLeaderboardText; // syuryoオブジェクトのTextコンポーネントを直接参照
    // --- 追加ここまで ---

    private bool gameStarted = false; 
    private bool isTimerDisplaying = false;

    void Start()
    {
        // カウントダウン中もimoとhaikeiは表示されたままにする
        if (imoObject != null) { imoObject.SetActive(true); }
        if (haikeiObject != null) { haikeiObject.SetActive(true); }

        // ゲームプレイのFireManagerは最初非表示
        if (fireManagerObject != null) { fireManagerObject.SetActive(false); }
        // syuryoObjectはGameOverPanelの子になるため、ここでは制御しない
        // if (syuryoObject != null) { syuryoObject.SetActive(false); } 

        // BakeManagerとImoDragはimoObjectにアタッチされているので、imoObjectが有効ならenabled状態はStartGameで切り替える
        if (bakeManager != null) { bakeManager.enabled = false; }
        if (imoDrag != null) { imoDrag.enabled = false; }

        // ゲーム終了UIは最初から非表示
        if (gameOverPanel != null) { gameOverPanel.SetActive(false); }
        if (retryButton != null) { retryButton.gameObject.SetActive(false); }
        if (nextStageButton != null) { nextStageButton.gameObject.SetActive(false); }
        
        // カウントダウンテキストは最初は非表示（InitialCountdownRoutineで表示される）
        if (countdownText != null) { countdownText.gameObject.SetActive(false); } 

        // OnlineScoreManagerも最初は無効化（必要に応じて）
        if (onlineScoreManager_01 != null) { onlineScoreManager_01.enabled = false; }
        
        // --- 変更点: OnlineScoreManagerにgameOverLeaderboardTextを渡す ---
        if (onlineScoreManager_01 != null && gameOverLeaderboardText != null)
        {
            onlineScoreManager_01.SetSyuryoTextComponent(gameOverLeaderboardText);
        }
        else if (onlineScoreManager_01 == null)
        {
            Debug.LogWarning("GameManager_01: OnlineScoreManager_01が設定されていません！");
        }
        else if (gameOverLeaderboardText == null)
        {
            Debug.LogWarning("GameManager_01: GameOverLeaderboardText (syuryo) が設定されていません！");
        }
        // --- 変更点ここまで ---

        StartCoroutine(StartInitialCountdownRoutine());
    }

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

        if (countdownText != null) { countdownText.gameObject.SetActive(false); }
        StartGame();
    }

    void StartGame()
    {
        gameStarted = true;
        Debug.Log("ゲーム開始！");

        if (fireManagerObject != null) { fireManagerObject.SetActive(true); }
        // syuryoObjectはGameOverPanelの子になるため、ここでは制御しない
        // if (syuryoObject != null) { syuryoObject.SetActive(true); } // 削除

        if (bakeManager != null) { bakeManager.enabled = true; }
        if (imoDrag != null) { imoDrag.enabled = true; }

        if (fireSpawner_01 != null)
        {
            fireSpawner_01.StartSpawningFires();
        }

        if (gameOverPanel != null) { gameOverPanel.SetActive(false); }
    }

    public void StartInGameTimerDisplay(float duration)
    {
        if (countdownText != null)
        {
            StopAllCoroutines();
            StartCoroutine(DisplayInGameTimerRoutine(duration));
            isTimerDisplaying = true;
        }
    }

    public void StopInGameTimerDisplay()
    {
        if (countdownText != null)
        {
            StopAllCoroutines();
            countdownText.gameObject.SetActive(false);
            isTimerDisplaying = false;
        }
    }

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
        isTimerDisplaying = false;
        
        if (bakeManager != null)
        {
            bakeManager.ForceEndGameByTimer();
        }
    }

    public void EndGame(bool success)
    {
        gameStarted = false;
        Debug.Log($"ゲーム終了！成功: {success}");

        // ゲームプレイオブジェクトと関連スクリプトを無効化
        if (imoObject != null) { imoObject.SetActive(false); }
        if (fireManagerObject != null) { fireManagerObject.SetActive(false); }
        // syuryoObjectはGameOverPanelの子になり、OnlineScoreManagerで制御するため、ここでは制御しない
        // if (syuryoObject != null) { syuryoObject.SetActive(false); } // 削除

        if (bakeManager != null) { bakeManager.enabled = false; }
        if (imoDrag != null) { imoDrag.enabled = false; }

        if (fireSpawner_01 != null)
        {
            fireSpawner_01.StopSpawningFires();
        }

        if (countdownText != null) { countdownText.gameObject.SetActive(false); }
        isTimerDisplaying = false; 

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

        if (bakeManager != null && onlineScoreManager_01 != null)
        {
            int finalScore = bakeManager.GetBakeScore();
            onlineScoreManager_01.SubmitGameScore(finalScore);
        }
        // 背景はゲーム終了後も表示されたままにする
        if (haikeiObject != null) { haikeiObject.SetActive(true); }
    }

    public void RestartGame()
    {
        Debug.Log("ゲームをリトライします...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToNextStage()
    {
        Debug.Log("次のステージへ進みます...");
        SceneManager.LoadScene("ibuki"); 
    }
}