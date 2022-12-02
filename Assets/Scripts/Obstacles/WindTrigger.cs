using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTrigger : MonoBehaviour
{
    public float power = 10;
    public float rotation = 0;

    private void Start()
    {
        transform.Rotate(0, rotation, 0);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody) other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * power);
    }
}
