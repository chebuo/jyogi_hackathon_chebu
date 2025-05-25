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

    //効果音用のAudioSource
    [SerializeField] public AudioSource hitSound;
    [SerializeField] public AudioSource missSound;
   
    void Start()
    {
        hitCounter = scoretext.GetComponent<HitCounter>();
        game = gggame.GetComponent<GAME>();

        if (hitSound == null || missSound == null)
        {
            Debug.LogError("効果音の AudioSource がセットされていません！ Inspector でアタッチしてください。");
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

                //効果音を再生
                if (hitSound != null) hitSound.Play();
            }
            else
            {
                Debug.Log("Miss");
                game.RegisterMiss();
                //効果音を再生
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

            //オブジェクトが破壊される際のMIss
            if (missSound != null) missSound.Play();
        }
    }

}
