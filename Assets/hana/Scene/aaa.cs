using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using JetBrains.Annotations; // �V�[���Ǘ��̂��߂ɕK�v

// �e���v���[�g�p��GameManager
// �Q�[���̊J�n�A�I���AUI�̕\���A�V�[���J�ڂȂǂ��Ǘ����܂��B
public class aaa : MonoBehaviour
{
    [Header("�� �J�E���g�_�E���ݒ�")]
    [Tooltip("�Q�[���J�n���ƁA�Q�[�����̓���̃C�x���g�i��F���������j�ŃJ�E���g�_�E����\������Text UI�R���|�[�l���g�������ɐݒ肵�܂��B")]
    public Text countdownText; // �J�E���g�_�E���\���p��Text UI
    [Tooltip("�Q�[���J�n���̃J�E���g�_�E���̎��ԁi�b�j��ݒ肵�܂��B")]
    public float initialCountdownDuration = 3f; // �Q�[���J�n���̃J�E���g�_�E������

    [Header("�� �Q�[���v���C����I�u�W�F�N�g")]
    [Tooltip("�Q�[���v���C���Ǘ�����I�u�W�F�N�g�������ɐݒ肵�܂��B�Q�[���J�n/�I�����ɗL��/������؂�ւ��܂��B")]
    public GameObject gameplayRootObject; // �Q�[���v���C�̃��[�g�ƂȂ�I�u�W�F�N�g�i��F�����imo�ȂǁA�Q�[���ŗL�̗v�f���܂Ƃ߂��e�I�u�W�F�N�g�j

    [Header("�� �Q�[���I��UI�ݒ�")]
    [Tooltip("�Q�[���I�����ɕ\�������p�l���i��FGameOverPanel�j�������ɐݒ肵�܂��B")]
    public GameObject gameOverPanel; // �Q�[���I���p�l��
    [Tooltip("���g���C�{�^���������ɐݒ肵�܂��B")]
    public Button retryButton; // ���g���C�{�^��
    [Tooltip("���̃X�e�[�W�֐i�ރ{�^���������ɐݒ肵�܂��B")]
    public Button nextStageButton; // ���̃X�e�[�W�{�^��

    private bool gameStarted = false; // �Q�[�����J�n���Ă��邩�ǂ����̃t���O

    //public Button StartButton;//�X�^�[�g�{�^��

    void Start()
    {
        // �Q�[���J�n���ɃQ�[���v���C�I�u�W�F�N�g���ꎞ�I�ɖ�����
        // �O������ݒ肳��Ȃ������ꍇ�ɔ�����null�`�F�b�N
        if (gameplayRootObject != null)
        {
            gameplayRootObject.SetActive(false);
        }

        // �Q�[���I��UI�͍ŏ������\���ɂ���
        if (gameOverPanel != null) { gameOverPanel.SetActive(false); }
        if (retryButton != null) { retryButton.gameObject.SetActive(false); }
        if (nextStageButton != null) { nextStageButton.gameObject.SetActive(false); }

        // �J�E���g�_�E���e�L�X�g�͍ŏ������\��
        if (countdownText != null) { countdownText.gameObject.SetActive(false); }

        // �Q�[���J�n���̃J�E���g�_�E�����J�n
        StartCoroutine(StartInitialCountdownRoutine());


        //if (StartButton != null) { StartButton.gameObject.SetActive(true); }
        //if (StartButton == null) { StartButton.gameObject.SetActive(true); }

        
    }
    
    // �Q�[���J�n���̃J�E���g�_�E���R���[�`��
    IEnumerator StartInitialCountdownRoutine()
    {
        float timer = initialCountdownDuration;
        if (countdownText != null) { countdownText.gameObject.SetActive(true); }

        while (timer > 0)
        {
            if (countdownText != null) { countdownText.text = Mathf.CeilToInt(timer).ToString(); }
            yield return new WaitForSeconds(1f);
            timer--;
        }

        if (countdownText != null) { countdownText.text = "�X�^�[�g�I"; }
        yield return new WaitForSeconds(1f);

        if (countdownText != null) { countdownText.gameObject.SetActive(false); }
        StartGame(); // �J�E���g�_�E���I����A�Q�[�����J�n
    }


    // �Q�[���J�n����
    void StartGame()
    {                gameStarted = true;
        Debug.Log("�Q�[���J�n�I");

        // �Q�[���v���C�I�u�W�F�N�g��L����
        if (gameplayRootObject != null)
        {
            gameplayRootObject.SetActive(true);

            // �����ŃQ�[���ŗL�̃��W�b�N�i��F�v���C���[����X�N���v�g�̗L�����Ȃǁj���Ăяo��
            // �Ⴆ�΁AImoDrag��BakeManager��enabled��true�ɂ��鏈���́A
            // gameplayRootObject���L���������Ƃ��ɁA���̎q�X�N���v�g�������I��Start()���Ă΂��悤�ɂ��邩�A
            // ��������GameManager�ł����̃X�N���v�g���Q�Ƃ���enabled��؂�ւ���悤�ɂ���B
            // �ėp���̂��߁A�����ł͒��ڎQ�Ƃ����ɁAgameplayRootObject���������Ǘ�����O��Ƃ���B
        }

        // �Q�[���I��UI�͔O�̂��߂����Ŕ�\����O��
        if (gameOverPanel != null) { gameOverPanel.SetActive(false); }

}
  

    // �Q�[�����̓���̃C�x���g�i��F�Ă������������ɋ߂Â������j�ŌĂяo�����^�C�}�[�\���J�n���\�b�h
    // duration: �^�C�}�[�̕b��
    public void StartInGameTimerDisplay(float duration)
    {
        if (countdownText != null)
        {
            StopAllCoroutines(); // ���ݎ��s���̂��ׂẴR���[�`�����~
            StartCoroutine(DisplayInGameTimerRoutine(duration)); // �V�����^�C�}�[�R���[�`�����J�n
        }
    }

   


    // �Q�[�����̃^�C�}�[�\���R���[�`��
    IEnumerator DisplayInGameTimerRoutine(float duration)
    {
        countdownText.gameObject.SetActive(true); // �J�E���g�_�E���e�L�X�g��\��
        float timer = duration;

        while (timer > 0)
        {
            countdownText.text = Mathf.CeilToInt(timer).ToString(); // �c��b����\��
            yield return new WaitForSeconds(1f);
            timer--;
        }
        countdownText.gameObject.SetActive(false); // �^�C�}�[��0�ɂȂ������\��

        // �^�C�}�[��0�ɂȂ�����Q�[�����I������i��̓I�ȃQ�[���I�����W�b�N�̓e���v���[�g���p���Œ�`�j
        Debug.Log("�Q�[�����^�C�}�[���I�����܂����B�Q�[���I���������Ăяo���܂��B");
        // �����ŃQ�[���I���i����/���s�j�̔�����s���AEndGame���Ăяo��
        // ��: BakeManager��ForceEndGameByTimer()�̂悤�ȃ��\�b�h���Ăяo���z��
        // �ėp�����l�����A�����ł͒���EndGame���Ăяo�����A�O������̃g���K�[��҂`�ɂ���
        // �������A�ł��V���v���Ȍ`�Ƃ��āA����EndGame���Ăяo�����Ƃ��\
        // EndGame(false); // �Ⴆ�΃^�C�}�[�I���͎��s�Ƃ��Ĉ����Ȃ�
    }

    // �Q�[�����̃^�C�}�[�\�����~���郁�\�b�h�i��F�΂��痣�ꂽ���j
    public void StopInGameTimerDisplay()
    {
        if (countdownText != null)
        {
            StopAllCoroutines(); // �^�C�}�[�̃R���[�`�����~
            countdownText.gameObject.SetActive(false); // �e�L�X�g���\���ɂ���
        }

    }


    

    // �O���i��F�Q�[���ŗL�̃��W�b�N�j����Ăяo�����Q�[���I�����\�b�h
    // success: �Q�[���������������ǂ���
    public void EndGame(bool success)
    {

        if (countdownText != null) { countdownText.gameObject.SetActive(false); }


        gameStarted = false; // �Q�[����Ԃ��I���ɐݒ�
        Debug.Log($"�Q�[���I���I����: {success}");

       

        // �Q�[���v���C�I�u�W�F�N�g�𖳌���
        if (gameplayRootObject != null)
        {
            gameplayRootObject.SetActive(false);
            // �����ŃQ�[���ŗL�̃��W�b�N�i��F�v���C���[����X�N���v�g�̖������Ȃǁj���Ăяo��
        }

        // �J�E���g�_�E���e�L�X�g����\���ɂ���
        if (countdownText != null) { countdownText.gameObject.SetActive(false); }

        // �Q�[���I��UI��\��
        if (gameOverPanel != null) { gameOverPanel.SetActive(true); }
        if (retryButton != null) { retryButton.gameObject.SetActive(true); } // ���g���C�{�^���͏�ɕ\��

        if (success)
        {
            if (nextStageButton != null) { nextStageButton.gameObject.SetActive(true); } // �����̏ꍇ�̂ݎ��̃X�e�[�W�{�^����\��
            Debug.Log("�Q�[�������I���̃X�e�[�W�֐i�߂܂��B");
        }
        else
        {
            if (nextStageButton != null) { nextStageButton.gameObject.SetActive(false); } // ���s�̏ꍇ�A���̃X�e�[�W�{�^���͔�\��
            Debug.Log("�Q�[�����s...���g���C���Ă��������B");
        }
    }


    // �Q�[���̃��g���C�����i�{�^������Ăяo���j
    public void RestartGame()
    {
        Debug.Log("�Q�[�������g���C���܂�...");
        // ���݂̃V�[�����ēǂݍ��݂��ăQ�[�������Z�b�g
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ���̃X�e�[�W�֐i�ޏ����i�{�^������Ăяo���j
    public void GoToNextStage()
    {
        Debug.Log("���̃X�e�[�W�֐i�݂܂�...");
        // ���ۂ̃Q�[���ł́A���̃V�[�����i��: "Stage2"�j���w��
        // ��: SceneManager.LoadScene("Stage2");
        // �����ł́A���݂̃V�[�����ēǂݍ��݂���_�~�[����
        SceneManager.LoadScene("chebuo");
    }
}