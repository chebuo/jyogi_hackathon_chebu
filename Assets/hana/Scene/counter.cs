using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class counter : MonoBehaviour
{
    private int count = 0 ;
    void OnMouseDown()
    {
        count++;
        Debug.Log($"{gameObject.name} がクリックされた！個別: {count} 回");
        // GameManager に全体カウントを通知
        if (GameManager.instance != null)
        {
            GameManager.instance.AddClick();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    private void Instantiate(object ballPrefab)
    {
        throw new NotImplementedException();
    }
   
}
