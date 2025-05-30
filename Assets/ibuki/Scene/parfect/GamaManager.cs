using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; // シーン管理のために必要

// テンプレート用のGameManager
// ゲームの開始、終了、UIの表示、シーン遷移などを管理します。
public class GamaManager : MonoBehaviour
{
    public Button nextStageButton;

    [Header("■ カウントダウン設定")]
    [Tooltip("ゲーム開始時と、ゲーム内の特定のイベント（例：完成判定後）でカウントダウンを表示するText UIコンポーネントをここに設定します。")]
    public Text countdownText; // カウントダウン表示用のText UI
    [Tooltip("ゲーム開始時のカウントダウンの時間（秒）を設定します。")]
    public float initialCountdownDuration = 3f; // ゲーム開始時のカウントダウン時間

    [Header("■ ゲームプレイ制御オブジェクト")]
    [Tooltip("管理するオブジェクトを設定 ゲーム開始/終了時に有効/無効を切り替えます。")]
    public GameObject hannteisenn1;// ゲームプレイのルートとなるオブジェクト（例：今回のimoなど、ゲーム固有の要素をまとめた親オブジェクト）
    public GameObject hannteisenn2;
    public GameObject hannteisenn3;
    

    private bool gameStarted = false; // ゲームが開始しているかどうかのフラグ

    void Start()
    {
        
        if (hannteisenn1 != null)
        {
            hannteisenn1.SetActive(false);
        }

        if (hannteisenn2 != null)
        {
            hannteisenn2.SetActive(false);
        }


        if (hannteisenn3 != null)
        {
            hannteisenn3.SetActive(false);
        }


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

        // ゲームプレイオブジェクトを有効化
        if (hannteisenn1 != null)
        {
            hannteisenn1.SetActive(true);
            // ここでゲーム固有のロジック（例：プレイヤー操作スクリプトの有効化など）を呼び出す
            // 例えば、ImoDragやBakeManagerのenabledをtrueにする処理は、
            // gameplayRootObjectが有効化されるときに、その子スクリプトが自動的にStart()を呼ばれるようにするか、
            // もしくはGameManagerでそれらのスクリプトを参照してenabledを切り替えるようにする。
            // 汎用性のため、ここでは直接参照せずに、gameplayRootObjectがそれらを管理する前提とする。
        }

        if (hannteisenn2 != null)
        {
            hannteisenn2.SetActive(true);
        }

        if (hannteisenn3 != null)
        {
            hannteisenn3.SetActive(true);
        }


        // ゲーム終了UIは念のためここで非表示を徹底
       
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
    public void EndGame(bool success)
    {
        gameStarted = false; // ゲーム状態を終了に設定
        Debug.Log($"ゲーム終了！成功: {success}");

        // ゲームプレイオブジェクトを無効化
        if (hannteisenn1 != null)
        {
            hannteisenn1.SetActive(false);
            // ここでゲーム固有のロジック（例：プレイヤー操作スクリプトの無効化など）を呼び出す
        }

        if (hannteisenn2 != null)
        {
            hannteisenn2.SetActive(false);
        }

        if (hannteisenn3 != null)
        {
            hannteisenn3.SetActive(false);
        }

        // カウントダウンテキストも非表示にする
        if (countdownText != null) { countdownText.gameObject.SetActive(false); }

        
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

   
}