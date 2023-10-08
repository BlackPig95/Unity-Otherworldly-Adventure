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
        if(colli == null)  
        colli = this.GetComponent<Collider2D>();
    }
    public void AttackPlayer(int damage)
    {
        int rayAtkCount = 7;
        float stepAtkY = this.colli.bounds.size.y / (rayAtkCount - 1);
        float yPos = this.transform.position.y - this.colli.bounds.size.y / 2;
        float atkCastRange = colli.bounds.size.x / 2 + 0.1f;
        for (int i = 1; i < rayAtkCount - 1; i++)
        {
            Vector2 castPosY = new Vector2(this.transform.position.x, yPos + i * stepAtkY);
            RaycastHit2D leftRay = Physics2D.Raycast(castPosY, Vector2.left, atkCastRange, layerMask);
            RaycastHit2D rightRay = Physics2D.Raycast(castPosY, Vector2.right, atkCastRange, layerMask);
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
        }
    }

    private void OnDrawGizmosSelected()
    {
        int rayAtkCount = 7;
        float stepAtkY = this.colli.bounds.size.y / (rayAtkCount - 1);
        float atkCastRange = colli.bounds.size.x / 2 + 0.1f;
        float yPos = this.transform.position.y - this.colli.bounds.size.y / 2;
        for (int i = 1; i < rayAtkCount - 1; i++)
        {
            Vector2 castPosY = new Vector2(this.transform.position.x, yPos + i * stepAtkY);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(castPosY, Vector2.left * atkCastRange);
            Gizmos.DrawRay(castPosY, Vector2.right * atkCastRange);

        }
    }
}
