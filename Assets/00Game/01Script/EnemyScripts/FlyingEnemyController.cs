using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlyingEnemyController : MonoBehaviour, ICanGetHit
{
    Rigidbody2D rigi;
    [SerializeField] float speed = 5.0f;
    int damage = 1, enemyHP = 100;
    Vector2 destination = Vector2.zero;
    int currentHP;
    SetEnemyDestination setDestin;
    AtkPlayerWithRay atkPlayerRay;
    EnemyAnimationController enemyAnimationController;

    // Start is called before the first frame update
    void Start()
    {
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
    }
}
