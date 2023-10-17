using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AtkPlayerWithRay))]
public class EnemyControllerWithRay : MonoBehaviour
{
    bool movingRight = true;
    [SerializeField] Stats stat;
    [SerializeField] float speed;
    [SerializeField] int damage;
    EnemyRayDown enemyRayDown;
    AtkPlayerWithRay atkPlayerRay;
    [SerializeField] Collider2D colli;
    Rigidbody2D rigi;
    int turnBack = 1;
    bool isRunning = false;
    DetectPlayerWithRay detectPlayer;

    public void OnEnable()
    {
        speed = stat.speed;
        damage = stat.damage;
        if (colli == null)
            colli = this.GetComponent<Collider2D>();
        enemyRayDown = this.GetComponent<EnemyRayDown>();
        atkPlayerRay = this.GetComponent<AtkPlayerWithRay>();
        rigi = this.GetComponent<Rigidbody2D>();
        detectPlayer = this.GetComponent<DetectPlayerWithRay>();
    }
    void Update()
    {
        EnemyRayMovement();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        atkPlayerRay.AttackPlayer(this.damage);
    }
    void EnemyRayMovement()
    {
        movingRight = enemyRayDown.EnemyDetectGround();
        isRunning = detectPlayer.DetectPlayer();
        if (!movingRight)
        {
            turnBack = 1;
            Vector2 scale = Vector2.one;
            scale.x = 1;
            this.transform.localScale = scale;

        }
        else
        {
            turnBack = -1;
            Vector2 scale = Vector2.one;
            scale.x = -1;
            this.transform.localScale = scale;
        }
        if (isRunning)
        {
            rigi.velocity = speed * Vector2.left * turnBack * 2;
            return;
        }
        rigi.velocity = speed * Vector2.left * turnBack;

    }
}
