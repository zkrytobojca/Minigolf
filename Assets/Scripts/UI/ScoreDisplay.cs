using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI text;
    private int score = 0;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        SimpleStrike.strike += UpdateStrike;
    }

    private void OnDisable()
    {
        SimpleStrike.strike -= UpdateStrike;
    }

    private void UpdateStrike()
    {
        score++;
        text.text = "Strikes: " + score;
    }
}
