using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;


public enum EnemyState
{
    Walk,
    Run,
    Hit,
}

[RequireComponent(typeof(AtkPlayerWithRay))]
public class EnemyController : MonoBehaviour
{
    Rigidbody2D rigi;
    [SerializeField] bool isRunning = false;
    [SerializeField] Stats stat;
    [SerializeField] float speed;
    [SerializeField] int damage;
    Vector2 destination = Vector2.zero;
    SetEnemyDestination setDestin;
    AtkPlayerWithRay atkPlayerRay;
    DetectPlayerWithRay detectPlayer;
    public void OnEnable()
    {
        speed = stat.speed;
        damage = stat.damage;
        atkPlayerRay = GetComponent<AtkPlayerWithRay>();
        setDestin = this.GetComponent<SetEnemyDestination>();
        rigi = this.GetComponent<Rigidbody2D>();
        detectPlayer = this.GetComponent<DetectPlayerWithRay>();
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
        isRunning = detectPlayer.DetectPlayer();
        destination = setDestin.SetDestin() - (Vector2)this.transform.position;
        if (!isRunning)
        {
            rigi.velocity = destination.normalized * speed; //Bug finish point can't change when move too fast
            return;
        }
        rigi.velocity = 2 * speed * destination.normalized;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
       atkPlayerRay.AttackPlayer(this.damage);
    }
    void RotateEnemy()
    {
        if (destination.x == 0) //Avoid bug NaN when init level
            return;
        Vector2 scale = Vector2.one;
        scale.x = -destination.normalized.x * 1/Mathf.Abs(destination.normalized.x);
        this.transform.localScale = scale;
    }
}
