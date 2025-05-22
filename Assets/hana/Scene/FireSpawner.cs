using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    public GameObject firePrefab;
    public float spawnInterval = 2f;
    public Vector2 spawnMin; // �����_���͈́i�����j
    public Vector2 spawnMax; // �����_���͈́i�E��j

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnFire();
            timer = 0;
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
}
