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
    // Start is called before the first frame update
    void Start()
    {
        InitLevel();
        this.Init();
        UIManager.Instance.Init();
    }
    void Init()
    {
        
    }
    public void PauseGame() //Let only gamemanager control game flow for easier reference/debug in future extension
    {
        if (gameState == GameState.Play)
            Time.timeScale = 1.0f;
        else if (gameState == GameState.Pause)
            Time.timeScale = 0.0f;
    }
    void InitLevel()
    {
        if (_playerController == null)
            _playerController = FindObjectOfType<PlayerController>();
        currentLevel++;
        if(oldLevel!= null)
            Destroy(oldLevel);

        oldLevel = Instantiate(levelPrefab[currentLevel]);
    }
}
