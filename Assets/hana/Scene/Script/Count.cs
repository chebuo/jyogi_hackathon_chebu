using JetBrains.Annotations;
using LootLocker.Requests;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Count : MonoBehaviour
{

    public static Count instance;
    private int totalClickCount = 0;
    public Text totalClickText;
    CountdownTimer countdown;
    private int Score = 0;
    public Text ScoreText;
    public Text Jage;

    string leaderboardID = "31020";
   
   
    void Awake()
    {

        countdown = GetComponent<CountdownTimer>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateUI();  // 初期表示
                     //if (ScoreText != null) { nextStageButton.gameObject.SetActive(false); }

    }

    public void AddClick()
    {
        if (countdown.currentTime>0)
        {
            totalClickCount++;
            Debug.Log($"クリック数: {totalClickCount}"); // デバッグ表示

            Score = totalClickCount * 20;
            Debug.Log($"スコア:{Score}");//デバッグ表示
            UpdateUI();
        }


    }
    private void Update()
    {
        //Debug.Log(countdown.currentTime);
    }
    void UpdateUI()
    {
       
        if (totalClickText != null)
        {
            totalClickText.text = $"Click:{totalClickCount}"; // テキストの更新
            ScoreText.text = (($"Score:{Score}"));


        }
        else
        {
            Debug.LogWarning("totalClickText が設定されていません！");
        }


        

        if (countdown.currentTime >=0)
        {
            if (Score< 540)
            {
                Jage.text = ("炎上");
            }
            else 
            {         
                Jage.text = ("消火成功");
                StartCoroutine(LoginAndSubmitScore(Score));

            }
        }


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
        LootLockerSDKManager.GetScoreList(leaderboardID, 100, (response) =>
        {
            if (response.success)
            {
                Debug.Log("スコアリスト取得成功！");
                foreach (var member in response.items)
                {
                    Debug.Log($"順位：{member.rank}|{member.score}");
                    //allscore += member.score;
                }
                //scoreLength = response.items.Length;
            }
            else
            {
                Debug.Log("スコアリスト取得失敗: " + response.errorData?.message);
            }
        });
        yield return new WaitUntil(() => done);
       /* scoreaverange = allscore / scoreLength;
        Debug.Log(scoreaverange);*/

    }





}