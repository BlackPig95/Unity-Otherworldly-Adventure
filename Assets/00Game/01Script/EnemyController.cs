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
    [SerializeField] LayerMask layerMask;
    Rigidbody2D rigi;
    EnemyState enemyState = EnemyState.Walk;
    [SerializeField] bool isRunning = false, isGettingHit = false;
    AnimationController enemyAnimationController;
    [SerializeField] Collider2D colli;
    [SerializeField] float speed = 5.0f;
    int damage = 1, enemyHP = 100;
    Vector2 destination = Vector2.zero;
    float currentHP;
    SetEnemyDestination setDestin;
    AtkPlayerWithRay atkPlayerRay;
    DetectPlayerWithRay detectPlayer;
    // Start is called before the first frame update
    void Start()
    {
        atkPlayerRay = GetComponent<AtkPlayerWithRay>();
        setDestin = this.GetComponent<SetEnemyDestination>();
        rigi = this.GetComponent<Rigidbody2D>();
        colli = this.GetComponent<Collider2D>();
        enemyAnimationController = this.GetComponentInChildren<AnimationController>();
        currentHP = this.enemyHP;
        enemyAnimationController.eventAnim += HitAnimEvent;
        detectPlayer = this.GetComponent<DetectPlayerWithRay>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateEnemy();
        SetEnemyState();
        enemyAnimationController.UpdateEnemyAnim(enemyState);
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
    void SetEnemyState()
    {
        if (isRunning)
            enemyState = EnemyState.Run;
        else enemyState = EnemyState.Walk;
        if(isGettingHit)
            enemyState = EnemyState.Hit;
    }
    public void HitAnimEvent(string name) //Attached to event action
    {
        if(name == CONSTANT.hitEvent)
        {
            isGettingHit = false;
            enemyState = EnemyState.Walk;
            Debug.Log(name);
        }
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
            isGettingHit = true;
        }
    }
  
}
