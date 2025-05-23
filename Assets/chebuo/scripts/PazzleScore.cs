using System.Collections;
using UnityEngine;
using LootLocker.Requests;

public class PazzleScore : MonoBehaviour
{
    GameObject[] pazzlePiece;
    public bool AllFinished;
    int i = 0;
    string leaderboardID = "30958";
    void Start()
    {
        pazzlePiece=GetAllPiece();
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
        foreach (GameObject piece in pazzlePiece)
        {          
            var script= piece.GetComponent<DragObject>();
            if(!script.finished)
            {
                Debug.Log(piece);
                AllFinished = false;
                break;
            }
        }
        if (AllFinished)
        {
            Debug.Log("finish");
            if (i == 0)
            {
                StartCoroutine(LoginAndSubmitScore(40));
                i++;
                Debug.Log("sousin");
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
    }
}
