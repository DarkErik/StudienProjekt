using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.playOnAwake = false;
        }
    }
    
    void Start()
    {
        //insert background music
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, (sound) => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }
        sound.source.Play();
    }

    public void Stop(string name)
    {
        Sound sound = Array.Find(sounds, (sound) => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }
        sound.source.Stop();
    }

    public void ChangeClip(string name, AudioClip clip)
    {
        Sound sound = Array.Find(sounds, (sound) => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }
        sound.source.clip = clip;
    }

    public void ChangeLoop(string name, bool loop)
    {
        Sound sound = Array.Find(sounds, (sound) => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }
        sound.source.loop = loop;
    }

    public void ChangeVolume(string name, float volume)
    {
        Sound sound = Array.Find(sounds, (sound) => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }
        if (volume < 0 || volume > 1)
            return;
        sound.source.volume = volume;
    }

    public void ChangePitch(string name, float pitch)
    {
        Sound sound = Array.Find(sounds, (sound) => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }
        if (pitch < 0 || pitch > 3)
            return;
        sound.source.pitch = pitch;
    }
}
