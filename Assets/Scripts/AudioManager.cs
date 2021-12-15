using System;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup audioMixer;
    public Sound[] sounds;

    // initialize all sounds
    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = audioMixer;
        }
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Play("MenuTheme");
        }
        else
        {
            Play("GameTheme");
        }
    }

    Sound FindSound (string name)
    {
        return Array.Find(sounds, sound => sound.name == name);
    }

    public void Play (string name)
    {
        Sound s = FindSound(name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = FindSound(name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void ChangeSoundSettings (string name, float altVolume, float altPitch)
    {
        Sound s = FindSound(name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.volume = altVolume;
        s.source.pitch = altPitch;
    }

    public void ChangeSoundSettings (string name, float value, int index)
        // Index 1 : Change volume to value
        // Index 2 : Change pitch to value
    {
        Sound s = FindSound(name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (index == 1)
        {
            if (value < 0f || value > 1f)
            {
                Debug.LogWarning("Volume change invalid for sound " + name);
                return;
            }
            s.source.volume = value;
        }
        else if (index == 2)
        {
            if (value < .1f || value > 3f)
            {
                Debug.LogWarning("Pitch change invalid for sound " + name);
                return;
            }
            s.source.pitch = value;
        }
        else
        {
            Debug.LogWarning("Settings index invalid for sound " + name);
        }
    }

    public void ResetSoundSettings (string name)
    {
        Sound s = FindSound(name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
    }

    public void ClickOnUIElements()
    {
        Sound s = FindSound("Click");
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
}
