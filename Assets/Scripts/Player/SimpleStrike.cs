using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SimpleStrike : MonoBehaviour
{
    public float power = 500;
    public float speedOfPowerChange = 850;
    public float sleepThreshold = 0.1f;
    Camera cameraMain;

    public GameObject arrowIndicatorPrefab;
    private GameObject arrowIndicator;
    public GameObject arrowPrefab;
    private Arrow arrowComponent;
    private GameObject arrow;
    private float arrowDistance = 0.07f;

    public float cooldown = 2;
    private float nextFireTime = 0;

    private Vector2 powerMinMax = new Vector2(15, 2000);

    public static event Action strike;
    public static event Action<float> powerChange;

    private void Start()
    {
        cameraMain = Camera.main;

        arrowIndicator = Instantiate(arrowIndicatorPrefab, transform.position, Quaternion.identity);
        arrowIndicator.transform.parent = transform.parent;
        
        arrow = Instantiate(arrowPrefab, transform.position, arrowIndicator.transform.rotation);
        arrowComponent = arrow.GetComponent<Arrow>();
    }

    private void Update()
    {
        if (MapSettings.isGamePaused == false)
        {
            ArrowIndicatorControl();
            PowerControl();
            StrikeMechanic();
        }
    }

    void StrikeMechanic()
    {
        if (InputManager.instance.KeyDown(Keybindings.ControlKey.Strike) && GetComponent<Rigidbody>().velocity.magnitude <= sleepThreshold)
        {
            if (Time.time > nextFireTime)
            {
                transform.parent.position = transform.position;
                transform.localPosition = Vector3.zero;
                ArrowIndicatorControl();
                GetComponent<Rigidbody>().AddForce((arrowIndicator.transform.position - transform.position).normalized * power);
                nextFireTime = Time.time + cooldown;
                FindObjectOfType<AudioManager>().PlayRandomSound("Strike");
                strike?.Invoke();
            }
        } 
    }

    void ArrowIndicatorControl()
    {
        arrowIndicator.transform.rotation = Quaternion.Euler(0, cameraMain.transform.eulerAngles.y, 0);

        arrowIndicator.transform.position = transform.position + arrowIndicator.transform.forward * arrowDistance;

        arrow.transform.rotation = arrowIndicator.transform.rotation;
        arrow.transform.position = transform.position + arrowIndicator.transform.forward * arrowDistance;

        if (GetComponent<Rigidbody>().velocity.magnitude <= sleepThreshold && Time.time > nextFireTime) arrow.SetActive(true);
        else arrow.SetActive(false);
    }

    void PowerControl()
    {
        if (InputManager.instance.KeyPressed(Keybindings.ControlKey.PowerDown))
        {
            power -= speedOfPowerChange * Time.deltaTime;
        }
        else if (InputManager.instance.KeyPressed(Keybindings.ControlKey.PowerUp))
        {
            power += speedOfPowerChange * Time.deltaTime;
        }
        power = Mathf.Clamp(power, powerMinMax.x, powerMinMax.y);
        arrowComponent.percentage = (power - powerMinMax.x) / (powerMinMax.y - powerMinMax.x);
        arrowComponent.UpdateModel();
        powerChange?.Invoke((power - powerMinMax.x) / (powerMinMax.y - powerMinMax.x));
    }
}
