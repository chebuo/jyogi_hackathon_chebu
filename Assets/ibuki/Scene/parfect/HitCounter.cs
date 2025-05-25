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

    // ƒqƒbƒg‚µ‚½‚Æ‚«‚ÉŒÄ‚Ño‚·
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
        hitCounterText.text = test + "‰ñ";
    }

    private void Update()
    {
        UpdateText();
        Debug.Log(currentHits);
    }
}
