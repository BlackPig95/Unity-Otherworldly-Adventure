using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AtkPlayerWithRay))]
public class FlyingEnemyController : MonoBehaviour, ICanGetHit
{
    Rigidbody2D rigi;
    Vector2 destination = Vector2.zero;
    [SerializeField] Stats stat;
    [SerializeField] float speed;
    [SerializeField] int damage, enemyHP;
    int currentHP;
    SetEnemyDestination setDestin;
    AtkPlayerWithRay atkPlayerRay;
    EnemyAnimationController enemyAnimationController;

    public void Init()
    {
        speed = stat.speed;
        damage = stat.damage;
        enemyHP = stat.hp;
        atkPlayerRay = GetComponent<AtkPlayerWithRay>();
        setDestin = this.GetComponent<SetEnemyDestination>();
        rigi = this.GetComponent<Rigidbody2D>();
        currentHP = this.enemyHP;
        enemyAnimationController = this.GetComponentInChildren<EnemyAnimationController>();
    }
    // Update is called once per frame
    void Update()
    {
        RotateEnemy();
    }
    private void FixedUpdate()
    {
        EnemyMovement();
    }
    void EnemyMovement()
    {
        destination = setDestin.SetDestin() - (Vector2)this.transform.position;
        rigi.velocity = destination.normalized * speed; //Bug finish point can't change when move too fast
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        atkPlayerRay.AttackPlayer(this.damage);
    }
    void RotateEnemy()
    {
        Vector2 scale = Vector2.one;
        scale.x = -destination.normalized.x;
        this.transform.localScale = scale;
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
