using System.Collections;
using UnityEngine;
using LootLocker.Requests;

public class lootlocker : MonoBehaviour
{
    string leaderboardID = "30958";
    string playerID="chebuo";
    void Start()
    {
        StartCoroutine(LoginAndSubmitScore(20));
    }

    public IEnumerator LoginAndSubmitScore(int score)
    {
        bool done = false;

        // ① ゲストログイン
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("ログイン成功！");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString()); // 保存してもOK
                done = true;
            }
            else
            {
                Debug.Log("ログイン失敗");
                done = true;
            }
        });

        // ログイン完了まで待機
        yield return new WaitUntil(() => done);

        // ② スコア送信
        string playerID = PlayerPrefs.GetString("PlayerID");
        done = false;

        LootLockerSDKManager.SubmitScore(playerID, score, leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("スコアアップロード成功！");
            }
            else
            {
                Debug.Log("スコアアップロード失敗: " + response.errorData?.message);
            }
            done = true;
        });

        yield return new WaitUntil(() => done);
    }
}
