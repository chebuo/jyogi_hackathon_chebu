using UnityEngine;
using UnityEngine.UI; // Textを使うために必要
using System.Collections; // Coroutineを使うために必要

public class GameManager : MonoBehaviour {
    [Header("カウントダウン設定")]
    public Text countdownText; // CountdownText UIへの参照をText型に変更
    public float countdownDuration = 3f; // カウントダウンの時間（秒）

    [Header("ゲームオブジェクト参照")]
    public GameObject imoObject; // imoオブジェクトへの参照
    public BakeManager bakeManager; // BakeManagerスクリプトへの参照

    private bool gameStarted = false;

    void Start() {
        // ゲーム開始時にimoオブジェクトとBakeManagerを無効化
        if (imoObject != null) {
            imoObject.SetActive(false);
        }
        if (bakeManager != null) {
            bakeManager.enabled = false;
        }

        // カウントダウンを開始
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown() {
        float timer = countdownDuration;
        if (countdownText != null) // nullチェックを追加
        {
            countdownText.gameObject.SetActive(true); // カウントダウンテキストを表示
        }

        while (timer > 0) {
            if (countdownText != null) // nullチェックを追加
            {
                countdownText.text = Mathf.CeilToInt(timer).ToString(); // 秒数を整数で表示
            }
            yield return new WaitForSeconds(1f); // 1秒待つ
            timer--;
        }

        if (countdownText != null) // nullチェックを追加
        {
            countdownText.text = "スタート！"; // 最後のメッセージ
        }
        yield return new WaitForSeconds(1f); // "スタート！"表示後1秒待つ

        if (countdownText != null) // nullチェックを追加
        {
            countdownText.gameObject.SetActive(false); // カウントダウンテキストを非表示にする
        }
        StartGame(); // ゲームを開始するメソッドを呼び出す
    }

    void StartGame() {
        gameStarted = true;
        Debug.Log("ゲーム開始！");

        // imoオブジェクトとBakeManagerを有効化
        if (imoObject != null) {
            imoObject.SetActive(true);
        }
        if (bakeManager != null) {
            bakeManager.enabled = true;
        }

        // ここにゲーム開始後の他の処理があれば追加
    }

    // ゲーム終了時に外部から呼び出すメソッド
    public void EndGame(bool success) {
        gameStarted = false;
        // ここにゲーム終了時のUI表示切り替えロジックを追加（後で実装）
        Debug.Log($"ゲーム終了！成功: {success}");

        // ここでリトライボタンや次のステージボタンを表示する処理を追加
    }
}