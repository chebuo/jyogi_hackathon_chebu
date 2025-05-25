using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;
using System.Linq;
using Unity.VisualScripting;

public class PazzleScore : MonoBehaviour
{
    [SerializeField] GameObject finishPanel;
    [SerializeField] GameObject score;
    [SerializeField] GameObject avescore;
    GameObject[] pazzlePiece;
    public bool AllFinished;
    int scoreaverange;
    int scoreLength;
    int allscore;
    int pazzlescore;
    int finishedPiece;
    int i = 0;
    string leaderboardID = "30958";
    AddScriptChild addScriptChild;
    Text scoretext;
    Text avetext;
    void Start()
    {
        addScriptChild = GameObject.Find("chebuhome_matome").GetComponent<AddScriptChild>();
        scoretext = score.GetComponent<Text>();
        avetext = avescore.GetComponent<Text>();
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
        AllFinished = true;
        pazzlescore = (int)addScriptChild.timer*50;
        Debug.Log(pazzlescore);
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
                    allscore += member.score;
                }
                scoreLength = response.items.Length;
                scoreaverange = allscore / scoreLength;
                Debug.Log(scoreaverange);
                scoretext.text = "SCORE:" + score;
                avetext.text = "AVERAGE:" + scoreaverange;
            }
            else
            {
                Debug.Log("�X�R�A���X�g�擾���s: " + response.errorData?.message);
            }
        });
        yield return new WaitUntil(() => done);
    }
}
