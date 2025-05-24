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
   
    void Start()
    {
        hitCounter = scoretext.GetComponent<HitCounter>();
        game = gggame.GetComponent<GAME>();
 
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
            }
            else
            {
                Debug.Log("Miss");
                game.RegisterMiss();
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
        }
    }

}
