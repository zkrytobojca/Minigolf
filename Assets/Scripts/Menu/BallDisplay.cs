using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerModel))]
public class BallDisplay : MonoBehaviour
{
    private void Start()
    {
        GetComponent<PlayerModel>().InstantiateDisplay(PlayerModel.skinId);
    }

    public void SelectNextBall()
    {
        PlayerModel.ChangeSkinId(1);
        UpdateBallDisplay();
    }

    public void SelectPreviousBall()
    {
        PlayerModel.ChangeSkinId(-1);
        UpdateBallDisplay();
    }

    public void SelectNextTrail()
    {
        PlayerModel.ChangeTrailId(1);
        UpdateBallDisplay();
    }

    public void SelectPreviousTrail()
    {
        PlayerModel.ChangeTrailId(-1);
        UpdateBallDisplay();
    }

    public void MoveToStart()
    {
        foreach (Transform child in transform)
        {
            child.position = transform.position;
        }
    }

    public void UpdateBallDisplay()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        GetComponent<PlayerModel>().InstantiateDisplay(PlayerModel.skinId);
    }
}
