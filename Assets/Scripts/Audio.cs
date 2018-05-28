using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Audio {

    public string name;

    public AudioClip audio;

    public bool diversify;
    public float pitchDelta;

    [Range(0f,1f)]
    public float volume;
    [Range(.1f,3f)]
    public float pitch;
    public bool loop;

    private AudioSource source = null;

    public void SetSource(AudioSource s)
    {
        source = s;
    }

    public AudioSource GetSource()
    {
        return source;
    }

    public void InitializeSource()
    {
        if (source == null) throw new System.Exception("AudioSource not set!");

        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
        source.clip = audio;
    }

}
