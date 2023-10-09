using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rigi;
    Collider2D colli;
    Vector2 bulletSpeed = new Vector2(300f, 200f);
    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
        colli = GetComponent<Collider2D>();
        rigi.AddForce(bulletSpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

}
