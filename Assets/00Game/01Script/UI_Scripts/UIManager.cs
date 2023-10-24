using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] Button volumeButton;
    [SerializeField] Button playButton;
    [SerializeField] Text finishedLevelText;
    //Load scene
    [SerializeField] Slider loadingBar;
    [SerializeField] List<Image> loadingScreen;
    [SerializeField] GameObject loadingCanvas;
    // Start is called before the first frame update
    private void Start()
    {
        volumeButton.onClick.AddListener(ChangeVolume);
        playButton.onClick.AddListener(PlayOrPause);
    }
    public void Init()
    {
        Observer.Instance.AddListener(Observer.FinishLevel, ShowText);
        Observer.Instance.AddListener(Observer.InitLevel, HideText);
        Observer.Instance.AddListener(Observer.InitLevel, (data)=>
        {
             StartCoroutine(WaitLoadScene());
        });
        if (volumeButton == null)
            volumeButton = GameObject.Find(CONSTANT.volumeButton).GetComponent<Button>();
        if (volumeSlider == null)
            volumeSlider = GameObject.Find(CONSTANT.volumeSlider).GetComponentInChildren<Slider>(true);
        if (playButton == null)
            playButton = GameObject.Find(CONSTANT.playButton).GetComponent<Button>();
       
    }
    IEnumerator WaitLoadScene()
    {
        float waitTime = 0f;
        StartCoroutine(GameManager.Instance.LoadingScreen());//Fake loading screen to transit better
        loadingCanvas.SetActive(true);
        for(int i = 0; i < loadingScreen.Count; i++)
        {
           if(i== GameManager.Instance.currentLevel)
                loadingScreen[i].gameObject.SetActive(true);
           else loadingScreen[i].gameObject.SetActive(false);
            
        }
        while (waitTime <= 1f)
        {
            waitTime += Time.unscaledDeltaTime;
            loadingBar.value = waitTime;
            yield return null;
        }
        loadingCanvas.SetActive(false);
    }
    public void ShowText(object data)
    {
        finishedLevelText.gameObject.SetActive(true);
    }
    public void HideText(object data)
    {
        finishedLevelText.gameObject.SetActive(false);
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
