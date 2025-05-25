using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class murasaki : MonoBehaviour
{
    private bool isInHitZone = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitZone"))
        {
            isInHitZone = true;
            Debug.Log("haitta");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HitZone"))
        {
            isInHitZone = false;
            Debug.Log("deta");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(isInHitZone ? "Hit" : "Miss");
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gameObject);
            return;
        }


    }

}
