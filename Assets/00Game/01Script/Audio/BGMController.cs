using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : Singleton<BGMController>
{
    [SerializeField] AudioClip[] clip;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Init()
    {
        if (GameManager.Instance.currentLevel != 2)
        {
            audioSource.clip = clip[0];
        }
        else audioSource.clip = clip[1];

        audioSource.Play();
    }
}
