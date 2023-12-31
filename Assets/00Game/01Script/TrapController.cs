using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    int damage = 1;
    bool movingRight = true;
    float speed = 5f;
    [SerializeField] Collider2D colli;
    // Start is called before the first frame update
    void Start()
    {
        if(colli == null)
        colli = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        TrapMovement();
    }
    private void FixedUpdate()
    {
    }
    void AttackPlayer(Collision2D collision)
    {
        ICanGetHit isGetHit = collision.collider.GetComponent<ICanGetHit>();
        isGetHit.GetHit(this.damage);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AttackPlayer(collision);
    }
    void TrapMovement()
    {
        this.transform.Translate(speed * Time.deltaTime * Vector2.right);

        if (movingRight)
            this.transform.eulerAngles = Vector3.zero;
        else
            this.transform.eulerAngles = new Vector3(0, 180, 0);

        float castRangeGround = colli.bounds.size.y / 2 + 0.5f;
        int rayCount = 8;
        float stepX = this.colli.bounds.size.x / (rayCount - 1);
        float xPos = this.transform.position.x - colli.bounds.size.x / 2;
        Vector2 castPosX1 = new Vector2(xPos - stepX, this.transform.position.y);
        Vector2 castPosX2 = new Vector2(xPos + rayCount * stepX, this.transform.position.y);
        RaycastHit2D detectGroundLeft = Physics2D.Raycast(castPosX1, Vector2.down, castRangeGround);
        RaycastHit2D detectGroundRight = Physics2D.Raycast(castPosX2, Vector2.down, castRangeGround);
        if (detectGroundLeft.collider == null)
        {
            movingRight = true;
        }
        else if (detectGroundRight.collider == null)
        {
            movingRight = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        float castRangeGround = colli.bounds.size.y / 2 + 0.5f;
        int rayCount = 8;
        float stepX = this.colli.bounds.size.x / (rayCount - 1);
        float xPos = this.transform.position.x - colli.bounds.size.x / 2;
        Vector2 castPosX1 = new Vector2(xPos - stepX, this.transform.position.y);
        Vector2 castPosX2 = new Vector2(xPos + rayCount*stepX, this.transform.position.y);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(castPosX1, Vector2.down * castRangeGround);
        Gizmos.DrawRay(castPosX2, Vector2.down * castRangeGround);
    }
}
