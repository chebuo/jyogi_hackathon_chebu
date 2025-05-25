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
        UpdateUI();  // �����\��
                     //if (ScoreText != null) { nextStageButton.gameObject.SetActive(false); }

    }

    public void AddClick()
    {
        if (countdown.currentTime>0)
        {
            totalClickCount++;
            Debug.Log($"�N���b�N��: {totalClickCount}"); // �f�o�b�O�\��

            Score = totalClickCount * 20;
            Debug.Log($"�X�R�A:{Score}");//�f�o�b�O�\��
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
            totalClickText.text = $"Click:{totalClickCount}"; // �e�L�X�g�̍X�V
            ScoreText.text = (($"Score:{Score}"));


        }
        else
        {
            Debug.LogWarning("totalClickText ���ݒ肳��Ă��܂���I");
        }


        

        if (countdown.currentTime >=0)
        {
            if (Score< 540)
            {
                Jage.text = ("����");
            }
            else 
            {         
                Jage.text = ("���ΐ���");
                StartCoroutine(LoginAndSubmitScore(Score));

            }
        }


    }
    public IEnumerator LoginAndSubmitScore(int score)
    {
        bool done = false;

        // �@ �Q�X�g���O�C��
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("���O�C�������I");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString()); // �ۑ����Ă�OK
                done = true;
            }
            else
            {
                Debug.Log("���O�C�����s");
                done = true;
            }
        });
        // ���O�C�������܂őҋ@
        yield return new WaitUntil(() => done);
        // �A �X�R�A���M
        string playerID = PlayerPrefs.GetString("PlayerID");
        done = false;

        LootLockerSDKManager.SubmitScore(playerID, score, leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("�X�R�A�A�b�v���[�h�����I");
            }
            else
            {
                Debug.Log("�X�R�A�A�b�v���[�h���s: " + response.errorData?.message);
            }
            done = true;
        });

        yield return new WaitUntil(() => done);
        LootLockerSDKManager.GetScoreList(leaderboardID, 100, (response) =>
        {
            if (response.success)
            {
                Debug.Log("�X�R�A���X�g�擾�����I");
                foreach (var member in response.items)
                {
                    Debug.Log($"���ʁF{member.rank}|{member.score}");
                    //allscore += member.score;
                }
                //scoreLength = response.items.Length;
            }
            else
            {
                Debug.Log("�X�R�A���X�g�擾���s: " + response.errorData?.message);
            }
        });
        yield return new WaitUntil(() => done);
       /* scoreaverange = allscore / scoreLength;
        Debug.Log(scoreaverange);*/

    }





}