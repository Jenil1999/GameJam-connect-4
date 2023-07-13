using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;
    public AudioSource BGsource;
    public AudioClip winsound;
    public static AudioManager AM;
    public AudioClip resetclip;
    public AudioClip losesound;

    private void Awake()
    {
        AM = this;
    }

    public void Playaudio(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    public void PlayWinSound()
    {
        source.clip = winsound;
        source.Play();
    }

    public void PlayLoseSound()
    {
        source.clip = losesound;
        source.Play();
    }

    public void PlayResetAudio()
    {
        source.clip = resetclip;
        source.Play();
    }

    public void Mute()
    {
        source.mute = true;
        BGsource.mute = true;
    }

    public void Unmute()
    {
        source.mute = false;
        BGsource.mute = false;
    }

}
