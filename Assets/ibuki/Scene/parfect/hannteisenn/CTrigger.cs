using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTrigger : MonoBehaviour
{
     HitCounter hitCounter;
    [SerializeField] GameObject scoretext;
    [SerializeField] GameObject gggame;
    GAME game;
    GameObject hannteisenn1;
    private bool isInHitZone = false;
    private bool destroyedByKey = false;

    //���ʉ��p��AudioSource
    [SerializeField] public AudioSource hitSound;
    [SerializeField] public AudioSource missSound;
   
    void Start()
    {
        hitCounter = scoretext.GetComponent<HitCounter>();
        game = gggame.GetComponent<GAME>();

        if (hitSound == null || missSound == null)
        {
            Debug.LogError("���ʉ��� AudioSource ���Z�b�g����Ă��܂���I Inspector �ŃA�^�b�`���Ă��������B");
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("hidari"))
        {
            isInHitZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("hidari"))
        {
            isInHitZone = false;
        }
    }


    private void Update()
    {
        hannteisenn1 = GameObject.FindWithTag("hidari");
        if (hannteisenn1 == null) return;
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (isInHitZone)
            {
                Debug.Log("Hit");
                hitCounter.AddHit();
                game.RegisterHit();

                //���ʉ����Đ�
                if (hitSound != null) hitSound.Play();
            }
            else
            {
                Debug.Log("Miss");
                game.RegisterMiss();
                //���ʉ����Đ�
                if (missSound != null) missSound.Play();
            }

            destroyedByKey = true;
            Destroy(hannteisenn1.gameObject);
        }
        Debug.Log(hannteisenn1);

    }
    private void OnDestroy()
    {
        if (!destroyedByKey)
        {
            Debug.Log("Miss");
            game.RegisterMiss();

            //�I�u�W�F�N�g���j�󂳂��ۂ�MIss
            if (missSound != null) missSound.Play();
        }
    }

}
