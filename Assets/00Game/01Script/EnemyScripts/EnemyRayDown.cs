using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyRayDown : MonoBehaviour
{
    Collider2D colli;
    bool movingRight = false;
    private void Start()
    {
        colli = this.GetComponent<Collider2D>();
    }
    public bool EnemyDetectGround()
    {
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
        return movingRight;
    }
}
