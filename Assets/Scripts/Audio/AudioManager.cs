using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public List<Sound> sounds;

    [HideInInspector]
    public static AudioManager instance;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
        
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.audioClip;
            sound.source.pitch = sound.pitch;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.looping;
            sound.source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[(int)sound.type+1];
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("volume")) audioMixer.SetFloat("Volume", Mathf.Log10(PlayerPrefs.GetFloat("volume")) * 20);
        if (PlayerPrefs.HasKey("volume_game")) audioMixer.SetFloat("VolumeGame", Mathf.Log10(PlayerPrefs.GetFloat("volume_game")) * 20);
        if (PlayerPrefs.HasKey("volume_ui")) audioMixer.SetFloat("VolumeUI", Mathf.Log10(PlayerPrefs.GetFloat("volume_ui")) * 20);
        if (PlayerPrefs.HasKey("volume_music")) audioMixer.SetFloat("VolumeMusic", Mathf.Log10(PlayerPrefs.GetFloat("volume_music")) * 20);

        PlaySound("Music");
    }

    public void PlaySound(string name)
    {
        Sound sound = sounds.Find(s => s.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound with name " + name + " not found!");
            return;
        }
        if (sound.source.loop == true && sound.source.isPlaying) return;
        sound.source.Play();
    }

    public void PlayRandomSound(string name)
    {
        List<Sound> soundList = sounds.FindAll(s => s.name.Contains(name));
        if (soundList == null)
        {
            Debug.LogWarning("Sound containing name " + name + " not found!");
            return;
        }
        soundList[Random.Range(0, soundList.Count)].source.Play();
    }
}
