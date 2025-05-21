using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitCounter : MonoBehaviour
{
    [SerializeField] Text hitCounterText;
    [SerializeField] private int maxHits = 30;

    int currentHits = 0;


    // �q�b�g�����Ƃ��ɌĂяo��
    public void AddHit()
    {
        if (currentHits < maxHits)
        {
            currentHits++;
            UpdateText();
            Debug.Log("a");
            Debug.Log("currentHits: " + currentHits);
        }
        
    }

    private void UpdateText()
    {
        int test = currentHits / maxHits;
        hitCounterText.text = test + "��";
    }

    private void Update()
    {
        UpdateText();
        Debug.Log(currentHits);
    }
}
