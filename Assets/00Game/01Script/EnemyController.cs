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
    [SerializeField] GameObject startPoint, endPoint;
    [SerializeField] LayerMask layerMask;
    Rigidbody2D rigi;
    EnemyState enemyState = EnemyState.Walk;
    [SerializeField] bool isRunning = false, isGettingHit = false;
    AnimationController enemyAnimationController;
    [SerializeField] Collider2D colli;
    [SerializeField] float speed = 5.0f, rayLength = 5.0f;
    int damage = 1, enemyHP = 100;
    [SerializeField] Vector2 finishPoint = Vector2.zero, destination = Vector2.zero;
    float currentHP;
    // Start is called before the first frame update
    void Start()
    {
        rigi = this.GetComponent<Rigidbody2D>();
        colli = this.GetComponent<Collider2D>();
        startPoint.transform.parent = null;
        endPoint.transform.parent = null;
        finishPoint = startPoint.transform.position;
        enemyAnimationController = this.GetComponentInChildren<AnimationController>();
        currentHP = this.enemyHP;
        enemyAnimationController.eventAnim += HitAnimEvent;
    }

    // Update is called once per frame
    void Update()
    {
        RotateEnemy();
        DetectPlayer();
        SetEnemyState();
        enemyAnimationController.UpdateEnemyAnim(enemyState);
    }
    private void FixedUpdate()
    {
        EnemyMovement();
    }

    void EnemyMovement()
    {
        if (Vector2.Distance(finishPoint, this.transform.position) <= 0.1f)
        {
            if (finishPoint == (Vector2)startPoint.transform.position)
                finishPoint = endPoint.transform.position;
            else finishPoint = startPoint.transform.position;
        }
        destination = finishPoint - (Vector2)this.transform.position;
        if (!isRunning)
        {
            rigi.velocity = destination.normalized * speed; //Bug finish point can't change when move too fast
            return;
        }
        rigi.velocity = destination.normalized * speed * 2;


    }
    void DetectPlayer()
    {
        int rayCount = 3;
        float stepY = this.colli.bounds.size.y / (rayCount-1);
        float yPos = this.transform.position.y - this.colli.bounds.size.y / 2;
        for (int i = 0; i < rayCount; i++)
        {
            Vector2 castPosY = new Vector2(this.transform.position.x, yPos + i * stepY);
            RaycastHit2D detectRay = Physics2D.Raycast(castPosY, destination, rayLength, layerMask);
            if (detectRay.collider != null)
                isRunning = true;
            else isRunning = false;
        }
    }
    void AttackPlayer()
    {
        int rayAtkCount = 7;
        float stepAtkY = this.colli.bounds.size.y / (rayAtkCount - 1);
        float yPos = this.transform.position.y - this.colli.bounds.size.y / 2;
        float atkCastRange = colli.bounds.size.x / 2 + 0.1f;
        for (int i = 1; i < rayAtkCount-1; i++)
        {
            Vector2 castPosY = new Vector2(this.transform.position.x, yPos + i * stepAtkY);
            RaycastHit2D leftRay = Physics2D.Raycast(castPosY, Vector2.left, atkCastRange, layerMask);
            RaycastHit2D rightRay = Physics2D.Raycast(castPosY, Vector2.right, atkCastRange, layerMask);
            if (leftRay.collider != null)
            {
                ICanGetHit isGetHit = leftRay.collider.GetComponent<ICanGetHit>();
                isGetHit.GetHit(this.damage);
                return;
            }
            if (rightRay.collider != null)
            {
                ICanGetHit isGetHit = rightRay.collider.GetComponent<ICanGetHit>();
                isGetHit.GetHit(this.damage);
                return;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AttackPlayer();
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
    private void OnDrawGizmosSelected()
    {
        int rayCount = 3;
        float stepY = this.colli.bounds.size.y / (rayCount-1);
        float yPos = this.transform.position.y - this.colli.bounds.size.y / 2;
        for (int i = 0; i < rayCount; i++)
        {
            Vector2 castPosY = new Vector2(this.transform.position.x, yPos + i * stepY);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(castPosY, destination.normalized * rayLength);
        }

        int rayAtkCount = 7;
        float stepAtkY = this.colli.bounds.size.y / (rayAtkCount - 1);
        float atkCastRange = colli.bounds.size.x / 2 + 0.1f;
        for (int i = 1; i < rayAtkCount-1; i++)
        {
            Vector2 castPosY = new Vector2(this.transform.position.x, yPos + i * stepAtkY);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(castPosY, Vector2.left * atkCastRange);
            Gizmos.DrawRay(castPosY, Vector2.right * atkCastRange);

        }
    }
}
