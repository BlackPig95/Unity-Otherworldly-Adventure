using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AtkPlayerWithRay : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] Collider2D colli;

    private void Awake()
    {
        if (colli == null)
            colli = this.GetComponent<Collider2D>();
        layerMask = LayerMask.GetMask(CONSTANT.playerPhysicsLayer);
    }
    public void AttackPlayer(int damage)
    {
        int rayAtkCount = 7;
        float stepAtkY = this.colli.bounds.size.y / (rayAtkCount - 1);
        float yPos = this.transform.position.y - this.colli.bounds.size.y / 2;
        float atkCastRange = 0.1f;

        float castRangeDown = colli.bounds.size.y / 2 + 0.02f;
        float stepX = this.colli.bounds.size.x / (rayAtkCount - 1);
        float xPos = this.transform.position.x - colli.bounds.size.x / 2;
        
        for (int i = 0; i < rayAtkCount; i++)
        {   //Raycast to sides
            Vector2 castPosYLeft = new Vector2(this.transform.position.x - colli.bounds.size.x/2, yPos + i * stepAtkY);
            Vector2 castPosYRight = new Vector2(this.transform.position.x + colli.bounds.size.x/2, yPos + i * stepAtkY);
            RaycastHit2D leftRay = Physics2D.Raycast(castPosYLeft, Vector2.left, atkCastRange, layerMask);
            RaycastHit2D rightRay = Physics2D.Raycast(castPosYRight, Vector2.right, atkCastRange, layerMask);
            //Raycast downward
            Vector2 castPosX = new Vector2(xPos + i * stepX, this.transform.position.y);
            RaycastHit2D rayDown = Physics2D.Raycast(castPosX, Vector2.down, castRangeDown, layerMask);
            if (leftRay.collider != null)
            {
                ICanGetHit isGetHit = leftRay.collider.GetComponent<ICanGetHit>();
                isGetHit.GetHit(damage);
                return;
            }
            if (rightRay.collider != null)
            {
                ICanGetHit isGetHit = rightRay.collider.GetComponent<ICanGetHit>();
                isGetHit.GetHit(damage);
                return;
            }
            if(rayDown.collider != null)
            {
                ICanGetHit isGetHit = rayDown.collider.GetComponent<ICanGetHit>();
                isGetHit.GetHit(damage);
                return;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        int rayAtkCount = 7;
        float stepAtkY = this.colli.bounds.size.y / (rayAtkCount - 1);
        float atkCastRange = 0.1f;
        float yPos = this.transform.position.y - this.colli.bounds.size.y / 2;

        float castRangeDown = colli.bounds.size.y / 2 + 0.02f;
        float stepX = this.colli.bounds.size.x / (rayAtkCount - 1);
        float xPos = this.transform.position.x - colli.bounds.size.x / 2;
        for (int i = 0; i < rayAtkCount; i++)
        {
            Vector2 castPosYLeft = new Vector2(this.transform.position.x - colli.bounds.size.x/2, yPos + i * stepAtkY);
            Vector2 castPosYRight = new Vector2(this.transform.position.x + colli.bounds.size.x /2, yPos + i * stepAtkY);

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(castPosYLeft, Vector2.left * atkCastRange);
            Gizmos.DrawRay(castPosYRight, Vector2.right * atkCastRange);

            Vector2 castPosX = new Vector2(xPos + i * stepX, this.transform.position.y);
            Gizmos.DrawRay(castPosX, Vector2.down * castRangeDown);
        }
    }
}
