using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBTN : MonoBehaviour
{
    public AudioClip _sound;

    public void Onclick()
    {
        AudioManager.AM.Playaudio(_sound);
    }
}