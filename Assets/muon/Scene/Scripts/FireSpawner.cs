using UnityEngine;
using System.Collections;
using System;

public class FireSpawner : MonoBehaviour
{
    [Header("■ 火のPrefab設定")]
    [Tooltip("生成する火のPrefab（FireZoneなど）をここに設定します。")]
    public GameObject firePrefab;

    [Header("■ 生成間隔と最大生成数")]
    [Tooltip("新しい火を生成する間隔の最小値（秒）")]
    public float minSpawnInterval = 2f; // 火を生成する間隔の最小値
    [Tooltip("新しい火を生成する間隔の最大値（秒）")]
    public float maxSpawnInterval = 3f; // 火を生成する間隔の最大値
    [Tooltip("同時に存在できる火の最大数")]
    public int maxFires = 3; // 同時に存在する火の最大数

    // --- 火の出現範囲設定 (四角形) ---
    [Header("■ 火の出現範囲設定 (四角形)")] // ヘッダーを四角形用に変更
    [Tooltip("火が出現するワールド座標の最小X値")]
    public float minX = -4f; // 出現範囲の最小X座標
    [Tooltip("火が出現するワールド座標の最大X値")]
    public float maxX = 4f; // 出現範囲の最大X座標
    [Tooltip("火が出現するワールド座標の最小Y値（上半分指定のため高めの値）")]
    public float minY = 0.5f; // 出現範囲の最小Y座標（画面中央より上）
    [Tooltip("火が出現するワールド座標の最大Y値")]
    public float maxY = 3.5f; // 出現範囲の最大Y座標
    // --- 変更ここまで ---

    private int currentFireCount = 0; // 現在存在する火の数

    void Start()
    {
        StartCoroutine(SpawnFireRoutine()); // 定期的に火を生成するコルーチンを開始
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
        // --- 四角形範囲内のランダムな位置を計算 ---
        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomY = UnityEngine.Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0); // 2DなのでZは0
        // --- 変更ここまで ---

        // Prefabを生成
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

    // デバッグ用に、Sceneビューで四角形の範囲を表示する
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // 四角形の中心点とサイズを計算
        Vector3 center = new Vector3((minX + maxX) / 2f, (minY + maxY) / 2f, 0);
        Vector3 size = new Vector3(maxX - minX, maxY - minY, 0);
        Gizmos.DrawWireCube(center, size);
    }
}