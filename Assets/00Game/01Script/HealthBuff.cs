using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Health")]
public class HealthBuff : PowerupEffects
{
    public int amount = 1;
    public override void ApplyPowerup(GameObject target)
    {
        target.GetComponent<PlayerController>().playerHP += amount;
    }
}
