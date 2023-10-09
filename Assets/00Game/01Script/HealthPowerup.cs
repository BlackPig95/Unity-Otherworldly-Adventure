using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Health")]
public class HealthPowerup : PowerupEffects
{
    public int amount;
    public override void ApplyPowerup(GameObject target)
    {
        target.GetComponent<PlayerController>().playerHP += amount;
    }
}
