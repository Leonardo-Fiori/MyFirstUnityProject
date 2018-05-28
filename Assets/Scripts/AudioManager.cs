using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

    public Audio[] sounds;

    public static AudioManager instance;

    private void Awake()
    {

        #region singleton
        DontDestroyOnLoad(gameObject);

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion

        // Inizializza la source dentro ciascun audio.
        // Non poteva essere creata direttamente dalla classe audio perchè è un componente che deve stare dentro
        // un gameobject, dunque la creo in AudioManager e poi la riferenzio dentro le classi Audio.
        // Poi da AudioManager cerco le classi audio e do play da li.
        foreach(Audio audio in sounds)
        {
            audio.SetSource(gameObject.AddComponent<AudioSource>());

            audio.InitializeSource();
        }
    }

    public void Play(string audioName)
    {
        Audio a = Array.Find(sounds, audio => audio.name == audioName);
        if (a.diversify)
        {
            a.GetSource().pitch = a.pitch + UnityEngine.Random.Range(-a.pitchDelta, a.pitchDelta);
            a.GetSource().Play();
            a.GetSource().pitch = a.pitch;
        }
        else
        {
            a.GetSource().Play();
        }
    }
}
