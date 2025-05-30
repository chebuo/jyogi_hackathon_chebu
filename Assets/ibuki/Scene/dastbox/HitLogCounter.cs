using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro対応


public class HitLogCounter : MonoBehaviour
{
    public TextMeshProUGUI hitCountText; // Inspectorで割り当て
    private int hitCount = 0;

    void OnEnable()
    {
        Application.logMessageReceived += OnLogMessageReceived;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= OnLogMessageReceived;
    }

    void OnLogMessageReceived(string logString, string stackTrace, LogType type)
    {
        if (logString.Contains("Hit"))
        {
            hitCount++;
            UpdateHitDisplay();
        }
    }

    void UpdateHitDisplay()
    {
        if (hitCountText != null)
        {
            hitCountText.text = hitCount.ToString();
        }
    }
}
