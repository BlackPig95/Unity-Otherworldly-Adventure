using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPowerup : MonoBehaviour
{
    [SerializeField] PowerupEffects Buff;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(CONSTANT.playerPhysicsLayer))
        {
            Buff.ApplyPowerup();
            Destroy(this.gameObject);
        }
    }
}
