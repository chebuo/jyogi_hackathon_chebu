using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSpawner : MonoBehaviour
{
    public GameObject hannteisenPrefab; // プレハブへの参照
    public Vector3 spawnPosition = Vector3.zero; // 生成位置
    public float minSpawnDelay = 1f; // 最小待機時間
    public float maxSpawnDelay = 5f; // 最大待機時間
    public int maxSpawnCount = 5;//生成回数の上限

    private GameObject currentHannteisen;
    private int spawnCount = 0;//現在の生成回数の記録

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (spawnCount<maxSpawnCount)//最大生成回数に達したらループ終了
        {
            if (currentHannteisen == null)
            {
                float waitTime = Random.Range(minSpawnDelay, maxSpawnDelay);
                yield return new WaitForSeconds(waitTime);

                currentHannteisen = Instantiate(hannteisenPrefab, spawnPosition, Quaternion.identity);
                spawnCount++;//生成回数をインクリメント
            }
            yield return null;
        }
    }
}
