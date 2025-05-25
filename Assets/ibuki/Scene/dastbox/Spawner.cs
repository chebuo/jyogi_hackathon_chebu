using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject hannteisenPrefab; // �v���n�u�ւ̎Q��
    public Vector3 spawnPosition = Vector3.zero; // �����ʒu
    public float minSpawnDelay = 1f; // �ŏ��ҋ@����
    public float maxSpawnDelay = 5f; // �ő�ҋ@����

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
