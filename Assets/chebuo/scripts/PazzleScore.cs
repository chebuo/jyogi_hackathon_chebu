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
