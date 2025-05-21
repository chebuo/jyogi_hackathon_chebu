using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NNNewBehaviourScript1 : MonoBehaviour


{
    [SerializeField] private HitCounter hitCounter;
    private bool isInHitZone = false;
    private bool destroyedByKey = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitZone"))
        {
            isInHitZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HitZone"))
        {
            isInHitZone = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
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
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(!destroyedByKey)
        {
            Debug.Log("Miss");
        } 

    }

}
