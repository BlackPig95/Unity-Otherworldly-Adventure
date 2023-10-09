using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPower : MonoBehaviour
{
    [SerializeField] PowerupEffects powerupEffects;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(CONSTANT.playerPhysicsLayer))
        {
            powerupEffects.ApplyPowerup(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
