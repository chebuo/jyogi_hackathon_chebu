using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NNNewBehaviourScript1 : MonoBehaviour
{
    HitCounter hitCounter;
    [SerializeField] GameObject scoretext;
    GameObject hannteisenn3;
    private bool isInHitZone = false;
    private bool destroyedByKey = false;


    void Start()
    {
        hitCounter = scoretext.GetComponent<HitCounter>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ue"))
        {
            isInHitZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ue"))
        {
            isInHitZone = false;
        }
    }

    private void Update()
    {
        hannteisenn3 = GameObject.FindWithTag("ue");
        if (hannteisenn3 == null) return;
        if (Input.GetKeyDown(KeyCode.W))
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
            Destroy(hannteisenn3.gameObject);
        }
        Debug.Log(hannteisenn3);
    }

    private void OnDestroy()
    {
        if(!destroyedByKey)
        {
            Debug.Log("Miss");
        } 

    }

}
