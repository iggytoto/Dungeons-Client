using System;
using TMPro;
using UnityEngine;

public class CurrentTimePanelController : MonoBehaviour
{
    public TMP_Text timeText;

    private float _lastUpdate = 2;

    private void Start()
    {
        UpdateTimeText();
    }

    public void Update()
    {
        _lastUpdate -= Time.deltaTime;
        if (!(_lastUpdate <= 0)) return;
        UpdateTimeText();
        _lastUpdate = 1;
    }

    private void UpdateTimeText()
    {
        var now = DateTime.Now;
        timeText.text = $"{now.Hour:00}:{now.Minute:00}";
    }
}