using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public int strikes = 0;
    public float endTime = 0;

    private void OnEnable()
    {
        SimpleStrike.strike += AddStrike;
        HoleTrigger.trigger += SetEndTIme;
    }

    private void OnDisable()
    {
        SimpleStrike.strike -= AddStrike;
        HoleTrigger.trigger -= SetEndTIme;
    }

    public void AddStrike()
    {
        strikes++;
    }

    public void SetEndTIme()
    {
        endTime = Timer.time;
    }
}
