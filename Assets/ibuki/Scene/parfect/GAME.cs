using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GAME : MonoBehaviour
{
    [Header("�Q�[���I��UI")]
    [Tooltip("�Q�[���I�����ɕ\�������p�l���ݒ�")]
    public GameObject gameOverPanel; // �Q�[���I���p�l��
    [Tooltip("���g���C�{�^���ݒ�")]
    public Button retryButton; // ���g���C�{�^��
    [Tooltip("���̃X�e�[�W�փ{�^���ݒ�")]
    public Button nextStageButton; // ���̃X�e�[�W�{�^��
    [Tooltip("�X�R�A�e�L�X�g")]
    public GameObject scoretext; // ���̃X�e�[�W�{�^��


    private int hitCount = 0;
    private int missCount = 0;
    private const int maxCount = 30; // ���v30��ŃQ�[���I��
    float time = 0;

    public static GAME Instance; // �V���O���g���p�^�[���ŃA�N�Z�X�\��

    void Start()
    {
        // �Q�[���I��UI�͍ŏ������\���ɂ���
        if (gameOverPanel != null) { gameOverPanel.SetActive(false); }
        if (retryButton != null) { retryButton.gameObject.SetActive(false); }
        if (nextStageButton != null) { nextStageButton.gameObject.SetActive(false); }
        if (scoretext != null) { scoretext.SetActive(false); }

    }
    void startGame()
    {
        if (gameOverPanel != null) { gameOverPanel.SetActive(false); }
        if (scoretext != null) { scoretext.SetActive(false); }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;//����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterHit()
    {
        hitCount++;
        CheckGameEnd();//����
    }

    public void RegisterMiss()
    {
        missCount++;
        CheckGameEnd();//����
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 40f)
        {
            EndGame();//dekita!!!!!
            Debug.Log("dekiteru");
        }
    }

     void CheckGameEnd()
    {
        int total = hitCount + missCount;
        if(total==15)
        {
            EndGame();//dekita!!!!!
            Debug.Log("dekiteru");
        }
    }

  

    private void EndGame()
    {
        Debug.Log("�Q�[�����I�����܂�");//dekita   but ���傢����

        // �Q�[���I��UI��\��
        if (gameOverPanel != null) { gameOverPanel.SetActive(true); }
        if (retryButton != null) { retryButton.gameObject.SetActive(true); } // ���g���C�{�^���͏�ɕ\��
        if (nextStageButton != null) { nextStageButton.gameObject.SetActive(true); }
        if (scoretext != null) { scoretext.SetActive(true); }
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}