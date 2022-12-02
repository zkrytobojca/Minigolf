using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public float rotationSpeed = 1, zoomSpeed = 0.1f;
    public Transform target;
    public float distanceFormTarget = 2;
    public Vector2 pitchMinMax = new Vector2(5, 85);
    public Vector2 zoomMinMax = new Vector2(0.4f, 3.5f);

    public float rotationSmoothTime = 0.25f;
    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;

    private float yaw;
    private float pitch;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (target == null)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length > 0) target = players[0].transform;
        }

    }

    private void LateUpdate()
    {
        if (MapSettings.isGamePaused == false) CamControl();
    }
    private void CamControl()
    {
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        if (Input.GetAxis("Mouse ScrollWheel") > 0) distanceFormTarget -= zoomSpeed;
        else if(Input.GetAxis("Mouse ScrollWheel") < 0) distanceFormTarget += zoomSpeed;
        distanceFormTarget = Mathf.Clamp(distanceFormTarget, zoomMinMax.x, zoomMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * distanceFormTarget;
    }
}
