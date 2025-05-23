using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTrigger : MonoBehaviour
{
     HitCounter hitCounter;
    [SerializeField] GameObject scoretext;
    GameObject hannteisenn1;
    private bool isInHitZone = false;
    private bool destroyedByKey = false;


    void Start()
    {
        hitCounter = scoretext.GetComponent<HitCounter>();
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
            }
            else
            {
                Debug.Log("Miss");
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
        }
    }

}
