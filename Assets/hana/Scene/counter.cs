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
        Debug.Log($"{gameObject.name} ���N���b�N���ꂽ�I��: {count} ��");
        // GameManager �ɑS�̃J�E���g��ʒm
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
