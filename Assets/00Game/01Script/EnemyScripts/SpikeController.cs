using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    int damage = 1;
    void AttackPlayer(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(CONSTANT.playerPhysicsLayer))
        {
            ICanGetHit isGetHit = collision.collider.GetComponent<ICanGetHit>();
            isGetHit.GetHit(this.damage);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        AttackPlayer(collision);
    }
}
