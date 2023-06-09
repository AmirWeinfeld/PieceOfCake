﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    //[Header("Player1")]
    //public AudioSource efxSource;//key channel Player1
    //public AudioSource moveEfxSource;//move channel Player1

    [Space]

    // [Header("Player2")]
    //public AudioSource efxSource2;//key channel Player2
    //public AudioSource moveEfxSource2;//move channel Player2

    [Header("General")]
    //public AudioSource deathEfxSource;
    public AudioSource musicSource;

    public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.   

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;


    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
        {
            //if not, set it to this.
            instance = this;
        }
        //If instance already exists:
        else if (instance != this)
        {
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);
        }
        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        //DontDestroyOnLoad(gameObject);
    }

    public void MuteMusic(bool toggle)
    {
        musicSource.mute = toggle;
    }

    //Used to play single sound clips.
    public void PlayEffect(AudioSource source, AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        source.clip = clip;

        //Play the clip.
        source.PlayDelayed(0);
    }

    public void PlayMove(AudioSource source, AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        source.clip = clip;

        //Play the clip.
        source.PlayDelayed(0);
    }

    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        //float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source to the randomly chosen pitch.
        //musicSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        musicSource.clip = clips[randomIndex];

        //Play the clip.
        musicSource.PlayDelayed(0);
    }
}
