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
            Play("MainTheme");
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
}
