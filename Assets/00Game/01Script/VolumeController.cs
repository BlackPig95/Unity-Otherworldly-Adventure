using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Slider volumeSlider;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (audioSource == null)
            audioSource = FindObjectOfType<AudioSource>();
        volumeSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = volumeSlider.value;
    }
}
