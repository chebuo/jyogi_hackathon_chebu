using System.Collections;
using Unity.VisualScripting; // 不要であれば削除してOK
using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    public GameObject firePrefab;
    public float spawnInterval = 2f;
    Vector2 spawnMin; // ランダム範囲（左下）
    Vector2 spawnMax; // ランダム範囲（右上）

    private float timer;
    private bool canSpawn = true; // 生成を許可するかどうかを制御するフラグ
    CountdownTimer countdown;




    private void Start()
    {
        spawnMax = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        spawnMin = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        countdown = GetComponent<CountdownTimer>();
        // CountdownTimerのイベントに登録
        CountdownTimer.OnCountdownFinished += StopSpawning;
       
    }

    private void OnDestroy()
    {
        // オブジェクトが破棄されるときにイベントの登録を解除する（重要！）
        CountdownTimer.OnCountdownFinished -= StopSpawning;
    }
    
    void Update()
    {
        // canSpawnがtrueの場合のみ生成処理を行う
        if (canSpawn)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                SpawnFire();
                timer = 0;
            }
        }
        
    }




    
    void SpawnFire()
    {
        Vector2 pos = new Vector2(
            Random.Range(spawnMin.x, spawnMax.x),
            Random.Range(spawnMin.y, spawnMax.y)
        );

        Instantiate(firePrefab, pos, Quaternion.identity);
    }

    // カウントダウン終了時に呼び出されるメソッド
    void StopSpawning()
    {
        canSpawn = false; // 生成を停止
        Debug.Log("FireSpawner: カウントダウンが0になったので火の生成を停止しました。");

    }


    


}