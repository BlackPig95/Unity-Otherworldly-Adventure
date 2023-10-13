using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerController _playerController;
    public PlayerController playerController => _playerController;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
