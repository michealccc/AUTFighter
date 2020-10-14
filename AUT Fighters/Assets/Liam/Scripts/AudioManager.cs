using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioManager Instance;

    public static bool BGMMute;

    public static bool FXMute;

    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }


    void Update()
    {
        foreach (Sound s in sounds)
        {
            if (s.bgm)
            {
                //s.source.volume = BGMVolume;
                s.source.mute = BGMMute;
            }
            else if (s.fx)
            {
                //s.source.volume = FXVolume;
                s.source.mute = FXMute;
            }
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("The sound: " + name + " was not found!");
            return;
        }
        s.source.Play();
    }

    public bool checkPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        {
            //if(s == null)
            //{
            //    return false;
            //}
            return s.source.isPlaying;
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Stop();
    }
}
