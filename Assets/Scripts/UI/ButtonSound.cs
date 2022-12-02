using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public void PlaySound(string name)
    {
        FindObjectOfType<AudioManager>().PlaySound(name);
    }
}
