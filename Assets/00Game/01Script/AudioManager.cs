using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource[] sound;

    public void PlayJumpSound()
    {
        sound[0].Play();
    }
    public void PlayHitSound()
    {
        sound[1].Play();
    }
}
