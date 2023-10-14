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
    [SerializeField] private PlayerController _playerController;
    public PlayerController playerController => _playerController;
    public GameState gameState = GameState.Play;
    // Start is called before the first frame update
    void Start()
    {
        if (_playerController == null) 
            _playerController = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        this.Init();
        UIManager.Instance.Init();
    }
    void Init()
    {
        _playerController.Init();
    }
    public void PauseGame() //Let only gamemanager control game flow for easier reference/debug in future extension
    {
        if (gameState == GameState.Play)
            Time.timeScale = 1.0f;
        else if(gameState == GameState.Pause)
            Time.timeScale = 0.0f;
    }
}
