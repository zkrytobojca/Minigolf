using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static float time = 0f;
    private static bool active = true;

    private void Start()
    {
        Restart();
    }

    private void Update()
    {
        if (active) time += Time.deltaTime;
    }

    public static void Activate()
    {
        active = true;
    }

    public static void Stop()
    {
        active = false;
    }

    public static void Restart()
    {
        time = 0f;
        Activate();
    }

    public static bool IsActive()
    {
        return active;
    }
}
