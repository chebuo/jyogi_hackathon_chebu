using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAME : MonoBehaviour
{
    private int hitCount = 0;
    private int missCount = 0;
    private const int maxCount = 30; // ���v30��ŃQ�[���I��

    public static GAME Instance; // �V���O���g���p�^�[���ŃA�N�Z�X�\��

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

    private void CheckGameEnd()
    {
        int total = hitCount + missCount;
        if (total >= maxCount)
        {
            EndGame();//����ĂȂ�
        }
    }

    private void EndGame()
    {
        Debug.Log("�Q�[�����I�����܂�");
        Application.Quit();
    }
}