using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    public ParticleSystem particles;
    public Vector3 offset;

    public static event Action trigger;

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(particles, transform.position + offset, particles.transform.rotation);
        particles.Play();

        FindObjectOfType<AudioManager>().PlaySound("BallInHole");

        trigger?.Invoke();
    }
}
