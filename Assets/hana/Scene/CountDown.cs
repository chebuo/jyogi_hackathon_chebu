using UnityEngine;
using UnityEngine.UI;
using System;
using JetBrains.Annotations; // Action���g�����߂ɕK�v

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 10f; // �J�E���g�_�E���̏�������
    public float currentTime;
    public Text timerText; // UI�ɕ\������ꍇ
    public GameObject retyButton;//���g���C�{�^��
    public GameObject nextButton;//�l�N�X�g�{�^��
    public GameObject gameOverPanel;//�Q�[���I�[�o�[�p�l��
   

    // �J�E���g�_�E����0�ɂȂ������Ƃ𑼂̃X�N���v�g�ɒʒm���邽�߂̃C�x���g
    // static�ɂ��邱�ƂŁACountdownTimer�̃C���X�^���X���Q�Ƃ����ɃC�x���g�ɓo�^�ł��܂�
    public static event Action OnCountdownFinished;

    void Start()
    {
        currentTime = countdownTime;
        UpdateTimerDisplay(); // �����\��
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();

            if (currentTime <= 0)
            {
                currentTime = 0;
                // �J�E���g�_�E�����I��������C�x���g�𔭉΂�����
                // ?.Invoke() �́A�C�x���g�ɓo�^����Ă��郁�\�b�h���Ȃ���Ή������Ȃ����S�ȌĂяo����
                OnCountdownFinished?.Invoke();
                Debug.Log("�J�E���g�_�E���I���I");
                this.retyButton.SetActive(true);
                this.nextButton.SetActive(true);
                this.gameOverPanel.SetActive(true);
               
            }
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + currentTime.ToString("F1");
        }
       
    }
}
