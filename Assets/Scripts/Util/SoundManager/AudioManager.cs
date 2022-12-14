using System;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;
    private string currentBGM;

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
            s.source.outputAudioMixerGroup = s.output;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.playOnAwake = false;
        }
    }
    
    void Start()
    {
        Play("BGM1");
        currentBGM = "BGM1";
    }

    public void Play(string name)
    {
        instance.PlayLogic(name);
    }

    private void PlayLogic(string name)
    {
        Sound sound = Array.Find(sounds, (sound) => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }
        if(sound != null)
            sound.source.Play();
    }

    public void Stop(string name)
    {
        instance.StopLogic(name);
    }

    private void StopLogic(string name)
    {
        Sound sound = Array.Find(sounds, (sound) => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }
        sound.source.Stop();
    }

    public void ChangeBackgroundMusic(string newTrack)
    {
        instance.ChangeBackgroundMusicLogic(newTrack);
    }

    private void ChangeBackgroundMusicLogic(string newTrack)
    {
        if (!newTrack.Equals(currentBGM))
            StartCoroutine(FadeTracks(newTrack));
    }

    private IEnumerator FadeTracks(string newTrack)
    {
        float timeToFade = 1.25f;
        float timer = 0f;

        Sound currentSound = Array.Find(sounds, (sound) => sound.name == currentBGM);
        Sound newSound = Array.Find(sounds, (sound) => sound.name == newTrack);
        if (newSound == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            yield break;
        }

        currentBGM = newTrack;
        newSound.source.Play();

        while (timer < timeToFade)
        {
            newSound.source.volume = Mathf.Lerp(0, 1, timer / timeToFade);
            currentSound.source.volume = Mathf.Lerp(1, 0, timer / timeToFade);
            timer += Time.deltaTime;
            yield return null;
        }

        newSound.source.volume = 1f;
        currentSound.source.volume = 0f;
    }

    public void ChangeClip(string name, AudioClip clip)
    {
        instance.ChangeClipLogic(name, clip);
    }

    private void ChangeClipLogic(string name, AudioClip clip)
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
        instance.ChangeLoopLogic(name, loop);
    }

    private void ChangeLoopLogic(string name, bool loop)
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
        instance.ChangeVolumeLogic(name, volume);
    }

    private void ChangeVolumeLogic(string name, float volume)
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
        instance.ChangeVolumeLogic(name, pitch);
    }

    private void ChangePitchLogic(string name, float pitch)
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
