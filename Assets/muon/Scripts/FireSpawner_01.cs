using UnityEngine;
using System.Collections;
using System;

public class FireSpawner_01 : MonoBehaviour // クラス名をFireSpawner_01に変更
{
    [Header("■ 火のPrefab設定")]
    [Tooltip("生成する火のPrefab（FireZoneなど）をここに設定します。")]
    public GameObject firePrefab;

    [Header("■ 生成間隔と最大生成数")]
    [Tooltip("新しい火を生成する間隔の最小値（秒）")]
    public float minSpawnInterval = 2f; 
    [Tooltip("新しい火を生成する間隔の最大値（秒）")]
    public float maxSpawnInterval = 3f; 
    [Tooltip("同時に存在できる火の最大数")]
    public int maxFires = 3; 

    [Header("■ 火の出現範囲設定 (四角形)")] 
    [Tooltip("火が出現するワールド座標の最小X値")]
    public float minX = -4f; 
    [Tooltip("火が出現するワールド座標の最大X値")]
    public float maxX = 4f; 
    [Tooltip("火が出現するワールド座標の最小Y値（上半分指定のため高めの値）")]
    public float minY = 0.5f; 
    [Tooltip("火が出現するワールド座標の最大Y値")]
    public float maxY = 3.5f; 

    private int currentFireCount = 0; 

    void Start()
    {
        StartCoroutine(SpawnFireRoutine()); 
    }

    IEnumerator SpawnFireRoutine()
    {
        while (true) 
        {
            float randomInterval = UnityEngine.Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(randomInterval);

            if (currentFireCount < maxFires) 
            {
                SpawnFire();
            }
        }
    }

    void SpawnFire()
    {
        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomY = UnityEngine.Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

        GameObject newFire = Instantiate(firePrefab, spawnPosition, Quaternion.identity);
        currentFireCount++;

        FireController fireController = newFire.GetComponent<FireController>();
        if (fireController != null)
        {
            fireController.OnFireDestroyed += DecrementFireCount;
        }
        else
        {
            Debug.LogWarning("生成された火のPrefabにFireControllerが見つかりません！");
        }
        
        Debug.Log($"火を生成しました: {newFire.name} at {spawnPosition}");
    }

    public void DecrementFireCount()
    {
        currentFireCount--;
        if (currentFireCount < 0) currentFireCount = 0;
        Debug.Log($"火が消滅しました。現在の火の数: {currentFireCount}");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 center = new Vector3((minX + maxX) / 2f, (minY + maxY) / 2f, 0);
        Vector3 size = new Vector3(maxX - minX, maxY - minY, 0);
        Gizmos.DrawWireCube(center, size);
    }
}