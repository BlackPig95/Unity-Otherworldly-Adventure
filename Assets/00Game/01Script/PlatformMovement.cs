using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    Vector2 destination = Vector2.zero;
    SetEnemyDestination setDestin;
    Rigidbody2D rigi;
    float speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        rigi = this.GetComponent<Rigidbody2D>();
        setDestin = this.GetComponent<SetEnemyDestination>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        MovePlatform();   
    }
    void MovePlatform()
    {
        destination = setDestin.SetDestin();
        this.transform.position = Vector2.MoveTowards(this.transform.position, destination, speed * Time.deltaTime);
    }
}
