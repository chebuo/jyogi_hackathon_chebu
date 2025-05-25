using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        Debug.Log(countdown.currentTime);
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


        

        if (countdown.currentTime ==0)
        {
            if (Score< 400)
            {
                Jage.text = ("���Ύ��s");
            }
            else 
            {         
                Jage.text = ("���ΐ���");
            }
        }


    }





   
}