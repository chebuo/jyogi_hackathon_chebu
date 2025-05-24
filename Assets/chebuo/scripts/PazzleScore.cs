using System.Collections;
using UnityEngine;
using LootLocker.Requests;
using System.Linq;

public class PazzleScore : MonoBehaviour
{
    [SerializeField] GameObject finishPanel;
    GameObject[] pazzlePiece;
    public bool AllFinished;
    int scoreaverange;
    int scoreLength;
    int allscore;
    int pazzlescore;
    int finishedPiece;
    int i = 0;
    string leaderboardID = "30958";
    void Start()
    {
        pazzlePiece=GetAllPiece();
        finishPanel.SetActive(false);
    }
    GameObject[] GetAllPiece()
    {
        var list = new System.Collections.Generic.List<GameObject>();
        foreach (GameObject AllObj in UnityEngine.Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (AllObj.layer == 3)
            {
                list.Add(AllObj);
            }
        }
        return list.ToArray();
    }
    void Update()
    {
        pazzlescore++;
        AllFinished = true;
       
        foreach (GameObject piece in pazzlePiece)
        {
            if (piece.GetComponent<DragObject>() == null) return;
            if (piece.GetComponent<DragObject>().finished == false)
            {
                AllFinished = false;
                finishedPiece++;
                break;
            }
        }
        if (AllFinished)
        {
            Debug.Log("finish");
            finishPanel.SetActive(true);
            if (i == 0)
            {
                StartCoroutine(LoginAndSubmitScore(pazzlescore));
                i++;
                Debug.Log("sousin");
            }
        }
    }
  /*  int GetAverange(int all,int score)
    {

    }*/
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
        LootLockerSDKManager.GetScoreList(leaderboardID, 100, (response) =>
        {
            if (response.success)
            {
                Debug.Log("スコアリスト取得成功！");
                foreach (var member in response.items)
                {
                    Debug.Log($"順位：{member.rank}|{member.score}");
                    allscore += member.score;
                }
                scoreLength = response.items.Length;
            }
            else
            {
                Debug.Log("スコアリスト取得失敗: " + response.errorData?.message);
            }
            scoreaverange=allscore / scoreLength;
            Debug.Log(scoreaverange);
        });
        yield return new WaitUntil(() => done);
    }
}
