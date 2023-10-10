using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPowerup : MonoBehaviour
{
    [SerializeField] PowerupEffects healthBuff;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(CONSTANT.playerPhysicsLayer))
        {
            healthBuff.ApplyPowerup(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
