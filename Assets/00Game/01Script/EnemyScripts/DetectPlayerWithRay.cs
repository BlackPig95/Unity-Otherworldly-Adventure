using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectPlayerWithRay : MonoBehaviour
{
    [SerializeField] Collider2D colli;
    bool isRunning = false;
    float rayLength = 5.0f;
    [SerializeField] LayerMask layerMask;
    Vector2 castDir = Vector2.left;
    private void Start()
    {
        colli = this.GetComponent<Collider2D>();
        layerMask = LayerMask.GetMask(CONSTANT.playerPhysicsLayer);
    }
    public bool DetectPlayer()
    {
        int direction = (int) Mathf.Sign(transform.rotation.y); //Raycast always go to front
        int rayCount = 3;
        float stepY = this.colli.bounds.size.y / (rayCount - 1);
        float yPos = this.transform.position.y - this.colli.bounds.size.y / 2;
        for (int i = 0; i < rayCount; i++)
        {
            Vector2 castPosY = new Vector2(this.transform.position.x, yPos + i * stepY);
            RaycastHit2D detectRay = Physics2D.Raycast(castPosY, castDir * transform.localScale.x * direction, rayLength, layerMask);
            if (detectRay.collider != null)
                isRunning = true;
            else isRunning = false;
        }
        return isRunning;
    }
    private void OnDrawGizmosSelected()
    {
        int direction = (int)Mathf.Sign(transform.rotation.y);
        int rayCount = 3;
        float stepY = this.colli.bounds.size.y / (rayCount - 1);
        float yPos = this.transform.position.y - this.colli.bounds.size.y / 2;
        for (int i = 0; i < rayCount; i++)
        {
            Vector2 castPosY = new Vector2(this.transform.position.x, yPos + i * stepY);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(castPosY, castDir.normalized * transform.localScale.x * rayLength * direction); //Raycast always go to front
        }
    }
}
