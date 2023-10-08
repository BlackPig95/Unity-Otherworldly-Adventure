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
public class EnemyController : MonoBehaviour, ICanGetHit
{
    Rigidbody2D rigi;
    [SerializeField] bool isRunning = false;
    [SerializeField] float speed = 5.0f;
    int damage = 1, enemyHP = 100;
    Vector2 destination = Vector2.zero;
    int currentHP;
    SetEnemyDestination setDestin;
    AtkPlayerWithRay atkPlayerRay;
    DetectPlayerWithRay detectPlayer;
    EnemyAnimationController enemyAnimationController;
    // Start is called before the first frame update
    void Start()
    {
        atkPlayerRay = GetComponent<AtkPlayerWithRay>();
        setDestin = this.GetComponent<SetEnemyDestination>();
        rigi = this.GetComponent<Rigidbody2D>();
        currentHP = this.enemyHP;
        detectPlayer = this.GetComponent<DetectPlayerWithRay>();
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
        isRunning = detectPlayer.DetectPlayer();
        destination = setDestin.SetDestin() - (Vector2)this.transform.position;
        if (!isRunning)
        {
            rigi.velocity = destination.normalized * speed; //Bug finish point can't change when move too fast
            return;
        }
        rigi.velocity = 2 * speed * destination.normalized;
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
        if(this.enemyHP < currentHP)
        {
            currentHP = this.enemyHP;
            enemyAnimationController.isGettingHit = true;
        }
    }
  
}
