using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro�Ή�


public class HitLogCounter : MonoBehaviour
{
    public TextMeshProUGUI hitCountText; // Inspector�Ŋ��蓖��
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
