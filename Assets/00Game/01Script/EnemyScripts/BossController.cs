using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public bool canJump = true, isGrounded = true;
    float maxHeight = 10f, maxRange = 8f, g = 9.81f;
    int damage = 1;
    public Transform playerPos;
    public Rigidbody2D rigi;
    AtkPlayerWithRay atkPlayer;
    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        atkPlayer = GetComponent<AtkPlayerWithRay>();
    }

    public void BossJump()
    {
        if (!canJump)
            return;
        Debug.Log("Jump");
        float distance = Mathf.Abs(playerPos.position.x - rigi.position.x);
        Vector2 direction = (Vector2)playerPos.position - rigi.position;
        /*    H = v ^ 2 * sin2a / g; R = v ^ 2 * sin2a / 2g => R * sin ^ 2a = H * sin2a => sin ^ 2a / sin2a = H / R
                   => sin ^ 2a / 2sinacosa = H / R => tana = 2H / R => a = atan(2H / R)
                   => v ^ 2 = Rg / sin2a; vx ^ 2 = v ^ 2 - vy ^ 2; vy = 10*/
        if (distance >= maxRange)
        {
            float angle = Mathf.Atan(2 * maxHeight / maxRange);
            float initialVelocity = Mathf.Sqrt(Mathf.Abs(maxRange * g / Mathf.Sin(2 * angle)));
            float yVelocity = initialVelocity * Mathf.Sin(angle);//Angle is in rad
            float xVelocity = initialVelocity * Mathf.Cos(angle);
            //If x distance is bigger than maxRange unit => Jump maxRange unit
            rigi.AddForce(new Vector2(direction.normalized.x * xVelocity, yVelocity), ForceMode2D.Impulse);
        }
        else if (distance < maxRange)
        {
            float angle = Mathf.Atan(2 * maxHeight / distance);
            float initialVelocity = Mathf.Sqrt(Mathf.Abs(distance * g / Mathf.Sin(2 * angle)));
            float yVelocity = initialVelocity * Mathf.Sin(angle);
            float xVelocity = initialVelocity * Mathf.Cos(angle);
            //If x distance is smaller than maxRange unit => Jump on player's head
            rigi.AddForce(new Vector2(direction.normalized.x * xVelocity, yVelocity), ForceMode2D.Impulse);
        }
        canJump = false;
        isGrounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(CONSTANT.groundTag))
        {
            isGrounded = true;
        }
        atkPlayer.AttackPlayer(damage);
    }
}
