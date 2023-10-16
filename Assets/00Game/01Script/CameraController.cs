using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] PlayerController playerPos;
    Vector3 offset = new Vector3(0, 0, -10);
    // Start is called before the first frame update
    void Start()
    {
        if(playerPos == null)
            playerPos = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
         this.transform.position = playerPos.transform.position + offset;
    }
}