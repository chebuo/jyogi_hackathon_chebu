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
            }
            else
            {
                Debug.Log("�X�R�A���X�g�擾���s: " + response.errorData?.message);
            }
            scoreaverange=allscore / scoreLength;
            Debug.Log(scoreaverange);
        });
        yield return new WaitUntil(() => done);
    }
}
