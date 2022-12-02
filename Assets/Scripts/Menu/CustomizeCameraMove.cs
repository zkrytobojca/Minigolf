using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeCameraMove : MonoBehaviour
{
    public float positionSmoothTime = 1.5f;
    private Vector3 positionSmoothVelocity;
    public float rotationSmoothTime = 1.5f;
    private Vector3 rotationSmoothVelocity;

    private bool moveTowardsBall = false;

    private Vector3 startPosition;
    private Vector3 currentPosition;
    private Vector3 endPosition = new Vector3(43.5f, 0.25f, 8.45f);

    private Vector3 startRotation;
    private Vector3 currentRotation;
    private Vector3 endRotation = new Vector3(16.5f, -17.2f, 0.0f);

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.eulerAngles;
        currentPosition = startPosition;
        currentRotation = startRotation;
    }

    private void LateUpdate()
    {
        if(moveTowardsBall)
        {
            currentRotation = Vector3.SmoothDamp(currentRotation, endRotation, ref rotationSmoothVelocity, rotationSmoothTime);
            transform.eulerAngles = currentRotation;

            currentPosition = Vector3.SmoothDamp(currentPosition, endPosition, ref positionSmoothVelocity, positionSmoothTime);
            transform.position = currentPosition;
        }
        else
        {
            currentRotation = Vector3.SmoothDamp(currentRotation, startRotation, ref rotationSmoothVelocity, rotationSmoothTime);
            transform.eulerAngles = currentRotation;

            currentPosition = Vector3.SmoothDamp(currentPosition, startPosition, ref positionSmoothVelocity, positionSmoothTime);
            transform.position = currentPosition;
        }
    }

    public void MoveCameraTowardsBall()
    {
        moveTowardsBall = true;
    }

    public void MoveCameraTowardsStart()
    {
        moveTowardsBall = false;
    }
}
