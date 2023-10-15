using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AtkPlayerWithRay))]
public class EnemyControllerWithRay : MonoBehaviour, ICanGetHit
{
    bool movingRight = true;
    int currentHP;
    [SerializeField] Stats stat;
    [SerializeField] float speed;
    [SerializeField] int damage, enemyHP;
    EnemyRayDown enemyRayDown;
    AtkPlayerWithRay atkPlayerRay;
    [SerializeField] Collider2D colli;
    Rigidbody2D rigi;
    int turnBack = 1;
    bool isRunning = false;
    DetectPlayerWithRay detectPlayer;
    EnemyAnimationController enemyAnimationController;

    public void Init()
    {
        speed = stat.speed;
        damage = stat.damage;
        enemyHP = stat.hp;
        currentHP = enemyHP;
        if (colli == null)
            colli = this.GetComponent<Collider2D>();
        enemyRayDown = this.GetComponent<EnemyRayDown>();
        atkPlayerRay = this.GetComponent<AtkPlayerWithRay>();
        rigi = this.GetComponent<Rigidbody2D>();
        detectPlayer = this.GetComponent<DetectPlayerWithRay>();
        enemyAnimationController = this.GetComponentInChildren<EnemyAnimationController>();
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
    public void GetHit(int damage)
    {
        this.enemyHP -= damage;
        Debug.Log("Enemy HP " + enemyHP);
        if (this.enemyHP < currentHP)
        {
            currentHP = this.enemyHP;
            enemyAnimationController.isGettingHit = true;
        }
        CheckDead();
    }
    void CheckDead()
    {
        if (this.enemyHP <= 0)
            Destroy(this.gameObject);
    }
}