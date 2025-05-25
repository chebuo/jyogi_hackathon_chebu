using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject hannteisenPrefab; // プレハブへの参照
    public Vector3 spawnPosition = Vector3.zero; // 生成位置
    public float minSpawnDelay = 1f; // 最小待機時間
    public float maxSpawnDelay = 5f; // 最大待機時間

    private GameObject currentHannteisen;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (currentHannteisen == null)
            {
                float waitTime = Random.Range(minSpawnDelay, maxSpawnDelay);
                yield return new WaitForSeconds(waitTime);

                currentHannteisen = Instantiate(hannteisenPrefab, spawnPosition, Quaternion.identity);
            }
            yield return null;
        }
    }
}
