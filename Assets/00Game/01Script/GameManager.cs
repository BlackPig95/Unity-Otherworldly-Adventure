using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        if (playerController == null) 
            playerController = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
