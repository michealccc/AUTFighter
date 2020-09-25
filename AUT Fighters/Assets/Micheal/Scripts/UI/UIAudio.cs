using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    private static AudioSource _audioSource;

    public void Play(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    private void Awake()
    {
        if (!_audioSource)
        {
            var go = new GameObject("UI Audio");

            _audioSource = go.AddComponent<AudioSource>();
            DontDestroyOnLoad(go);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
