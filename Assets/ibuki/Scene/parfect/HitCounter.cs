using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitCounter : MonoBehaviour
{
    [SerializeField] Text hitCounterText;
    [SerializeField] private int maxHits = 15;

    public int currentHits = 0;

    void Start()
    {
        currentHits = 0;
    }

    // ヒットしたときに呼び出す
    public void AddHit()
    {
        if (currentHits < maxHits)
        {
            currentHits++;
            UpdateText();
            Debug.Log("currentHits: " + currentHits);
        }
        
    }

    private void UpdateText()
    {
        string test =$" {currentHits} / {maxHits}";
        hitCounterText.text = test + "回";
    }

    private void Update()
    {
        UpdateText();
        Debug.Log(currentHits);
    }
}
