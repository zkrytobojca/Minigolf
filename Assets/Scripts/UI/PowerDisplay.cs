using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDisplay : MonoBehaviour
{
    public Vector2 barMaxMin = new Vector2(5, 150);
    public float barWidth = 40;

    private void OnEnable()
    {
        SimpleStrike.powerChange += UpdatePower;
    }

    private void OnDisable()
    {
        SimpleStrike.powerChange -= UpdatePower;
    }

    private void UpdatePower(float powerPercentage)
    {
        float barHeight = Mathf.Clamp(barMaxMin.y * powerPercentage, barMaxMin.x, barMaxMin.y);
        GetComponent<RectTransform>().sizeDelta  = new Vector2(barWidth, barHeight);
    }
}
