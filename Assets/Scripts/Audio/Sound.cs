using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public enum SoundType
    {
        Game,
        UI,
        Music
    }

    public string name;

    public AudioClip audioClip;

    public SoundType type;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch;

    public bool looping;

    [HideInInspector]
    public AudioSource source;
}
