using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class yyyyyyyyy : MonoBehaviour
{
    public Text hitsText; 
    [SerializeField] GameObject Hittext;
    HitCounter hitCounter;
    
    void Start()
    {
        hitCounter = Hittext.GetComponent<HitCounter>();
        if (hitsText == null)
        {
            Debug.LogError("hitsText ���A�^�b�`����Ă��܂���I");
        }
    }

    void Update()
    {   
            if (hitsText != null)
            {
                hitsText.text = "Score: " + (hitCounter.currentHits * 66);
            }
    }
}