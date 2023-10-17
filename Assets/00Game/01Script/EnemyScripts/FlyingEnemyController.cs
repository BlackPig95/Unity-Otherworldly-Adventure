using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AtkPlayerWithRay))]
public class FlyingEnemyController : MonoBehaviour
{
    Rigidbody2D rigi;
    Vector2 destination = Vector2.zero;
    [SerializeField] Stats stat;
    [SerializeField] float speed;
    [SerializeField] int damage;
    SetEnemyDestination setDestin;
    AtkPlayerWithRay atkPlayerRay;

    public void Start()
    {
        speed = stat.speed;
        damage = stat.damage;
        atkPlayerRay = GetComponent<AtkPlayerWithRay>();
        setDestin = this.GetComponent<SetEnemyDestination>();
        rigi = this.GetComponent<Rigidbody2D>();
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        atkPlayerRay.AttackPlayer(this.damage);
    }
    void RotateEnemy()
    {
        if (destination.x == 0)
            return;
        Vector2 scale = Vector2.one;
        scale.x = -destination.normalized.x * 1 / Mathf.Abs(destination.normalized.x); //Scale magnitude always 1
        this.transform.localScale = scale;
    }
}
