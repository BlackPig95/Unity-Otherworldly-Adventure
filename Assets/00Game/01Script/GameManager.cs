using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Play,
    Pause,
}
public class GameManager : Singleton<GameManager>
{
    public int currentLevel = -1;
    public GameObject oldLevel;
    [SerializeField] List<GameObject> levelPrefab = new List<GameObject>();
    [SerializeField] private PlayerController _playerController;
    private bool applicationQuitting = false;
    public Canvas UIcanvas;
    public PlayerController playerController
    {
        get
        {
            if (_playerController == null)
                _playerController = FindObjectOfType<PlayerController>();
            return _playerController;
        }
    }
    public GameState gameState = GameState.Play;
    private void Awake()
    {
        UIcanvas.gameObject.SetActive(false);
        if (_playerController == null)
            _playerController = FindObjectOfType<PlayerController>();
        Observer.Instance.AddListener(Observer.InitLevel, InitLevel);
    }
    // Start is called before the first frame update
    void Start()
    {
        Observer.Instance.Notify(Observer.InitLevel);
    }

    public void PauseGame() //Let only gamemanager control game flow for easier reference/debug in future extension
    {
        if (gameState == GameState.Play)
            Time.timeScale = 1.0f;
        else if (gameState == GameState.Pause)
            Time.timeScale = 0.0f;
    }
    void InitLevel(object data = null)
    {
        if (currentLevel >= levelPrefab.Count-1)
            currentLevel = -1;
        currentLevel++;
        if(oldLevel!= null)
        {
            Destroy(oldLevel);
            Destroy(_playerController.gameObject); //Avoid bug player not get deleted together with level prefab
        }
        StartCoroutine(WaitEndFrame());
        StartCoroutine(WaitUI());
    }
    public IEnumerator LoadingScreen()
    {
        gameState = GameState.Pause;
        PauseGame();
        yield return new WaitForSecondsRealtime(1f);
        gameState = GameState.Play;
        PauseGame();
    }
    private void OnDestroy()
    {
        if (!applicationQuitting)
            Observer.Instance.RemoveListener(Observer.InitLevel, InitLevel);
    }
    private void OnApplicationQuit()
    {
        applicationQuitting = true;
    }
    IEnumerator WaitUI()
    {
        yield return new WaitForSecondsRealtime(0.2f);//Give some time to finish instantiate new level prefab
        UIManager.Instance.Init();
        CameraController.Instance.Init();
    }
    IEnumerator WaitEndFrame()
    {
        yield return new WaitForEndOfFrame(); //Wait for old level to be completely deleted before instantiate new one
        oldLevel = Instantiate(levelPrefab[currentLevel]);

        if (_playerController != null)//Delete old script
            _playerController = null;
        _playerController = FindObjectOfType<PlayerController>();
        Observer.Instance.Notify(Observer.InitComponent);
        UIcanvas.gameObject.SetActive(true);
    }
}
