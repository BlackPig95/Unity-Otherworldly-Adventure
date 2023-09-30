using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public enum EnemyState
{
    Walk, 
    Run,
    Hit,
}
public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject startPoint, endPoint;
    [SerializeField] LayerMask layerMask;
    Rigidbody2D rigi;
    BoxCollider2D colli;
    [SerializeField] float speed = 5.0f, rayLength;
    [SerializeField] Vector2 finishPoint = Vector2.zero, destination = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        rigi = this.GetComponent<Rigidbody2D>();
        colli = this.GetComponent<BoxCollider2D>();
        startPoint.transform.parent = null;
        endPoint.transform.parent = null;
        finishPoint = startPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        RotateEnemy();
        DetectPlayer();
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
        rigi.velocity = destination.normalized * speed; //Bug finish point can't change when move too fast
    }
    void DetectPlayer()
    {
        RaycastHit2D detectRay = Physics2D.Raycast(this.transform.position, destination, rayLength, layerMask);
        if (detectRay.collider != null)
            Debug.Log(detectRay.collider.name);
    }
    void RotateEnemy()
    {
        Vector2 scale = Vector2.one;
        scale.x = -destination.normalized.x;
        this.transform.localScale = scale;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, destination.normalized * rayLength);
    }
}
