using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAME : MonoBehaviour
{
    private int hitCount = 0;
    private int missCount = 0;
    private const int maxCount = 30; // 合計30回でゲーム終了

    public static GAME Instance; // シングルトンパターンでアクセス可能に

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;//成功
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterHit()
    {
        hitCount++;
        CheckGameEnd();//成功
    }

    public void RegisterMiss()
    {
        missCount++;
        CheckGameEnd();//成功
    }

    private void CheckGameEnd()
    {
        int total = hitCount + missCount;
        if (total >= maxCount)
        {
            EndGame();//されてない
        }
    }

    private void EndGame()
    {
        Debug.Log("ゲームを終了します");
        Application.Quit();
    }
}