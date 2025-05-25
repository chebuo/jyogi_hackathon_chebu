using UnityEngine;

public class TriggerHit2D : MonoBehaviour
{
    private bool isInHitZone = false;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(isInHitZone ? "Hit" : "Miss");
        }
    }
}
