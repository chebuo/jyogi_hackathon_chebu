using UnityEngine;
using System.Collections;
using LootLocker.Requests;
using UnityEngine.UI; 
using System.Linq; 

public class OnlineScoreManager_01 : MonoBehaviour 
{
    [Header("■ LootLocker設定")]
    [Tooltip("LootLockerで設定したリーダーボードのID。")]
    public string leaderboardID = "31019"; 

    private bool isSubmittingScore = false; 

    private Text syuryoText; 
    public void SetSyuryoTextComponent(Text textComponent) 
    {
        syuryoText = textComponent;
        if (syuryoText != null)
        {
            syuryoText.gameObject.SetActive(false); 
        }
    }

    private int _playerLastSubmittedScore = 0; 

    void Start()
    {
    }

    public void SubmitGameScore(int finalScore)
    {
        _playerLastSubmittedScore = finalScore; 
        Debug.Log($"OnlineScoreManager_01: プレイヤーの最終スコアを保存しました: {_playerLastSubmittedScore}"); // 追加
        if (!isSubmittingScore) 
        {
            isSubmittingScore = true;
            StartCoroutine(LoginAndSubmitScore(finalScore));
        }
    }

    IEnumerator LoginAndSubmitScore(int scoreToSubmit)
    {
        bool done = false;

        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("LootLocker ログイン成功！Player ID: " + response.player_id);
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.LogError("LootLocker ログイン失敗: " + response.errorData?.message);
                done = true;
            }
        });
        yield return new WaitUntil(() => done);

        if (!LootLockerSDKManager.CheckInitialized())
        {
            Debug.LogError("LootLocker SDKが初期化されていません。スコア送信を中止します。");
            isSubmittingScore = false;
            yield break;
        }

        string playerID = PlayerPrefs.GetString("PlayerID");
        done = false;

        Debug.Log($"OnlineScoreManager_01: スコア送信を試みます。スコア: {scoreToSubmit}, リーダーボードID: {leaderboardID}"); // 追加
        LootLockerSDKManager.SubmitScore(playerID, scoreToSubmit, leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("スコアアップロード成功！送信スコア: " + scoreToSubmit);
                GetLeaderboardList(); 
            }
            else
            {
                Debug.LogError("スコアアップロード失敗: " + response.errorData?.message);
            }
            done = true;
        });
        yield return new WaitUntil(() => done);

        isSubmittingScore = false;
    }

    public void GetLeaderboardList()
    {
        Debug.Log("OnlineScoreManager_01: リーダーボードリストの取得を開始します。"); // 追加
        StartCoroutine(GetLeaderboardListRoutine());
    }

    IEnumerator GetLeaderboardListRoutine()
    {
        bool done = false;
        string leaderboardDisplay = ""; // リーダーボードリストは表示しないので空に
        float totalScore = 0;
        int scoreCount = 0;
        float averageScore = 0;
        string averageComparisonMessage = "";

        LootLockerSDKManager.GetScoreList(leaderboardID, 100, (response) => 
        {
            if (response.success)
            {
                Debug.Log("OnlineScoreManager_01: スコアリスト取得成功！"); // 追加
                if (response.items.Length > 0)
                {
                    foreach (var member in response.items)
                    {
                        // リーダーボードリストの表示は行わないため、leaderboardDisplayへの追加は削除
                        // string memberName = (member.player != null && !string.IsNullOrEmpty(member.player.name)) ? member.player.name : $"プレイヤー{member.member_id}";
                        // leaderboardDisplay += $"{member.rank}. {memberName}: {member.score}\n";
                        
                        totalScore += member.score; 
                        scoreCount++; 
                    }

                    if (scoreCount > 0)
                    {
                        averageScore = totalScore / scoreCount;
                        Debug.Log($"OnlineScoreManager_01: リーダーボード平均スコア: {averageScore}"); // 追加

                        if (_playerLastSubmittedScore > averageScore)
                        {
                            averageComparisonMessage = $"あなたのスコア ({_playerLastSubmittedScore}) は平均 ({averageScore:F0}) より上です！";
                        }
                        else if (_playerLastSubmittedScore < averageScore)
                        {
                            averageComparisonMessage = $"あなたのスコア ({_playerLastSubmittedScore}) は平均 ({averageScore:F0}) より下です。";
                        }
                        else
                        {
                            averageComparisonMessage = $"あなたのスコア ({_playerLastSubmittedScore}) は平均 ({averageScore:F0}) と同じです。";
                        }
                    }
                    else
                    {
                        averageComparisonMessage = "まだスコアがありません。あなたが最初のスコアです！";
                    }
                }
                else
                {
                    averageComparisonMessage = "まだスコアがありません。あなたが最初のスコアです！";
                }
                
                if (syuryoText != null)
                {
                    syuryoText.text = averageComparisonMessage; 
                    syuryoText.gameObject.SetActive(true); 
                    Debug.Log($"OnlineScoreManager_01: syuryoTextにメッセージを設定し、表示しました: {averageComparisonMessage}"); // 追加
                }
            }
            else // スコアリスト取得失敗
            {
                Debug.LogError("OnlineScoreManager_01: スコアリスト取得失敗: " + response.errorData?.message); // 追加
                averageComparisonMessage = "スコアの比較はできませんでした。";

                if (syuryoText != null)
                {
                    syuryoText.text = averageComparisonMessage; 
                    syuryoText.gameObject.SetActive(true); 
                    Debug.Log($"OnlineScoreManager_01: syuryoTextにエラーメッセージを設定し、表示しました: {averageComparisonMessage}"); // 追加
                }
            }
            done = true;
        });
        yield return new WaitUntil(() => done);
    }
}