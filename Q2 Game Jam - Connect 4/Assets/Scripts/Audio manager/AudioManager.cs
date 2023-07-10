using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;
    public static AudioManager AM;
    public AudioClip winclip;
    public AudioClip resetclip;

    private void Awake()
    {
        AM = this;
    }

    public void Playaudio(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    public void PlayWinAudio()
    {
        source.clip = winclip;
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
    }

    public void Unmute()
    {
        source.mute = false;
    }

}
