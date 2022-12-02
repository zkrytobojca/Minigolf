using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimeDisplay : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private string TimeFormat(float time)
    {
        string result = "";
        float seconds = Mathf.FloorToInt(time % 60);
        float minutes = Mathf.FloorToInt(time / 60);
        float hours = Mathf.FloorToInt(time / 3600);

        if (time >= 3600) result += hours + "h ";
        if (time >= 60) result += minutes + "min ";
        result += seconds + "s";
        return result;
    }

    private void Update()
    {
        if (Timer.IsActive()) text.text = "Time: " + TimeFormat(Timer.time);
    }
}
