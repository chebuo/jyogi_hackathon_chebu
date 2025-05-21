using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSpawner : MonoBehaviour
{
    public GameObject hannteisenPrefab; // �v���n�u�ւ̎Q��
    public Vector3 spawnPosition = Vector3.zero; // �����ʒu
    public float minSpawnDelay = 1f; // �ŏ��ҋ@����
    public float maxSpawnDelay = 5f; // �ő�ҋ@����
    public int maxSpawnCount = 5;//�����񐔂̏��

    private GameObject currentHannteisen;
    private int spawnCount = 0;//���݂̐����񐔂̋L�^

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (spawnCount<maxSpawnCount)//�ő吶���񐔂ɒB�����烋�[�v�I��
        {
            if (currentHannteisen == null)
            {
                float waitTime = Random.Range(minSpawnDelay, maxSpawnDelay);
                yield return new WaitForSeconds(waitTime);

                currentHannteisen = Instantiate(hannteisenPrefab, spawnPosition, Quaternion.identity);
                spawnCount++;//�����񐔂��C���N�������g
            }
            yield return null;
        }
    }
}
