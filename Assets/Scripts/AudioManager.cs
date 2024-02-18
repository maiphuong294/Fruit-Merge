using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource soundAudioSource;
    public AudioSource musicAudioSource;

    [Header("Music")]
    public AudioClip homeMusic;
    public AudioClip playMusic;
    public AudioClip pauseMusic;

    [Header("Sound")]
    public AudioClip buttonClick;
    public AudioClip gameOver;
    public AudioClip dropObject;


    public void PlaySound()
    {

    }
}
