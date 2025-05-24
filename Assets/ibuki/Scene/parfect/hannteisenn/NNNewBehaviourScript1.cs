using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NNNewBehaviourScript1 : MonoBehaviour
{
    HitCounter hitCounter;
    [SerializeField] GameObject scoretext;
    [SerializeField] GameObject gggggame;
    GAME game;
    GameObject hannteisenn3;
    private bool isInHitZone = false;
    private bool destroyedByKey = false;

    void Start()
    {

        if (scoretext == null)
        {
            Debug.LogError("scoretext が Inspector でアタッチされていません！");
        }
        else
        {
            hitCounter = scoretext.GetComponent<HitCounter>();
        }

        if (gggggame == null)
        {
            Debug.LogError("gggggame が Inspector でアタッチされていません！");
        }
        else
        {
            game = gggggame.GetComponent<GAME>();
        }
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
                game.RegisterHit();
            }
            else
            {
                Debug.Log("Miss");
                game.RegisterMiss();
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
            game.RegisterMiss();
        } 

    }

}
