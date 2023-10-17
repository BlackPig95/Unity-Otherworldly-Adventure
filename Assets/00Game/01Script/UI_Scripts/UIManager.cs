using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] Button volumeButton;
    [SerializeField] Button playButton;
    // Start is called before the first frame update
    public void Init()
    {
        Observer.Instance.AddListener(Observer.FinishLevel, ShowText);
        if (volumeButton == null)
            volumeButton = GameObject.Find(CONSTANT.volumeButton).GetComponent<Button>();
        if (volumeSlider == null)
            volumeSlider = GameObject.Find(CONSTANT.volumeSlider).GetComponentInChildren<Slider>(true);
        if (playButton == null)
            playButton = GameObject.Find(CONSTANT.playButton).GetComponent<Button>();
        volumeButton.onClick.AddListener(ChangeVolume); 
        playButton.onClick.AddListener(PlayOrPause);
    }
    public void ShowText(object data)
    {
        GameObject textObject = (GameObject)data;
        textObject.SetActive(true);
    }
    public void PlayOrPause()
    {
        if (GameManager.Instance.gameState == GameState.Play)
        {
            playButton.image.color = Color.blue;
            GameManager.Instance.gameState = GameState.Pause;
            GameManager.Instance.PauseGame();
        }
        else if (GameManager.Instance.gameState == GameState.Pause)
        {
            playButton.image.color = Color.white;
            GameManager.Instance.gameState = GameState.Play;
            GameManager.Instance.PauseGame();
        }
    }
    public void ChangeVolume()
    {
        volumeSlider.gameObject.SetActive(!volumeSlider.isActiveAndEnabled);
    }
}
