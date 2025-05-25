using System.Collections;
using Unity.VisualScripting; // �s�v�ł���΍폜����OK
using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    public GameObject firePrefab;
    public float spawnInterval = 2f;
    Vector2 spawnMin; // �����_���͈́i�����j
    Vector2 spawnMax; // �����_���͈́i�E��j

    private float timer;
    private bool canSpawn = true; // �����������邩�ǂ����𐧌䂷��t���O
    CountdownTimer countdown;




    private void Start()
    {
        spawnMax = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        spawnMin = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        countdown = GetComponent<CountdownTimer>();
        // CountdownTimer�̃C�x���g�ɓo�^
        CountdownTimer.OnCountdownFinished += StopSpawning;
       
    }

    private void OnDestroy()
    {
        // �I�u�W�F�N�g���j�������Ƃ��ɃC�x���g�̓o�^����������i�d�v�I�j
        CountdownTimer.OnCountdownFinished -= StopSpawning;
    }
    
    void Update()
    {
        // canSpawn��true�̏ꍇ�̂ݐ����������s��
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

    // �J�E���g�_�E���I�����ɌĂяo����郁�\�b�h
    void StopSpawning()
    {
        canSpawn = false; // �������~
        Debug.Log("FireSpawner: �J�E���g�_�E����0�ɂȂ����̂ŉ΂̐������~���܂����B");

    }


    


}