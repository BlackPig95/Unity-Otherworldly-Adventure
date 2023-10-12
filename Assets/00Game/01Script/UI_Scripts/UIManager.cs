using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] Button playButton;
    // Start is called before the first frame update
    void Start()
    {
        Observer.Instance.AddListener(Observer.FinishLevel, ShowText);
    }
    public void ShowText(object data)
    {
       GameObject textObject = (GameObject)data;
            textObject.SetActive(true);
    }
    public void PlayOrPause()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            playButton.image.color = Color.blue;
        }
        else
        {
            Time.timeScale = 1;
            playButton.image.color = Color.white;
        }
    }
    public void ChangeVolume()
    {
        volumeSlider.gameObject.SetActive(!volumeSlider.isActiveAndEnabled);
    }
}
