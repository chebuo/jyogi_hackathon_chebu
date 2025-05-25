using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    HitCounter hitCounter;
    [SerializeField] GameObject scoretext;
    [SerializeField] GameObject ggggame;
    GAME game;
    GameObject hannteisenn2;
    private bool isInHitZone = false;
    private bool destroyedByKey=false;

    //効果音用のAudioSource
    [SerializeField] public AudioSource hitSound;
    [SerializeField] public AudioSource missSound;

    void Start()
    {
        hitCounter = scoretext.GetComponent<HitCounter>();
        game = ggggame.GetComponent<GAME>();

        if(hitSound==null ||  missSound==null)
        {
            Debug.LogError("効果音未セット");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("migi"))
        {
            isInHitZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("migi"))
        {
            isInHitZone = false;
        }
    }

    private void Update()
    {
        hannteisenn2 = GameObject.FindWithTag("migi");
        if (hannteisenn2 == null) return;
        if (Input.GetKeyDown(KeyCode.D))
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
            Destroy(hannteisenn2.gameObject);
        }
        Debug.Log(hannteisenn2);
    }

    private void OnDestroy()
    {
        if(!destroyedByKey)
        {
            Debug.Log("Miss");
            game.RegisterMiss();

            if (missSound != null) missSound.Play();
        }
    }
}


