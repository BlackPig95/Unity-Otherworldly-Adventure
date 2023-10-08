using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerWithRay : MonoBehaviour
{
    readonly int damage = 1;
    bool movingRight = true;
    readonly float speed = 5f;
    EnemyRayDown enemyRayDown;
    AtkPlayerWithRay atkPlayerRay;
    [SerializeField] Collider2D colli;
    Rigidbody2D rigi;
    int turnBack = 1;
    bool isRunning = false;
    DetectPlayerWithRay detectPlayer;

    private void Awake()
    {
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        atkPlayerRay.AttackPlayer(this.damage);
    }
    void EnemyRayMovement()
    {
        movingRight = enemyRayDown.EnemyDetectGround();
        isRunning = detectPlayer.DetectPlayer();
        if (!movingRight)
        {
            this.transform.eulerAngles = Vector3.zero;
            turnBack = 1;
        }
        else
        {
            this.transform.eulerAngles = new Vector3(0, -180, 0); //The rotation is reversed??
            turnBack = -1;
        }
        if (isRunning)
        {
            rigi.velocity = speed * Vector2.left * turnBack * 2;
            return;
        }
        rigi.velocity = speed * Vector2.left * turnBack;

    }
    private void OnDrawGizmosSelected()
    {
        float castRangeGround = colli.bounds.size.y / 2 + 0.5f;
        int rayCount = 8;
        float stepX = this.colli.bounds.size.x / (rayCount - 1);
        float xPos = this.transform.position.x - colli.bounds.size.x / 2;
        Vector2 castPosX1 = new Vector2(xPos - stepX, this.transform.position.y);
        Vector2 castPosX2 = new Vector2(xPos + rayCount * stepX, this.transform.position.y);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(castPosX1, Vector2.down * castRangeGround);
        Gizmos.DrawRay(castPosX2, Vector2.down * castRangeGround);
    }
}
