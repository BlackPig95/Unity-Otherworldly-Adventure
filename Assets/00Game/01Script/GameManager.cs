using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        gameState = GameState.Play;
        PauseGame();
        currentLevel++;
        if(oldLevel!= null)
        {
            Destroy(oldLevel);
            Destroy(_playerController);
        }

        oldLevel = Instantiate(levelPrefab[currentLevel]);

        if (_playerController == null)
            _playerController = FindObjectOfType<PlayerController>();
        StartCoroutine(Wait());
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
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(1f);
        UIManager.Instance.Init();
        CameraController.Instance.Init();
        Debug.Log("Wait");
    }
}
