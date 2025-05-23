using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    HitCounter hitCounter;
    [SerializeField] GameObject scoretext;
    GameObject hannteisenn2;
    private bool isInHitZone = false;
    private bool destroyedByKey=false;


    void Start()
    {
        hitCounter = scoretext.GetComponent<HitCounter>();
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
            }
            else
            {
                Debug.Log("Miss");
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
        }
    }
}


