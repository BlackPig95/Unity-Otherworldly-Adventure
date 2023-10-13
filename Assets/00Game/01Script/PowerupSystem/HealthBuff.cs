using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Health")]
public class HealthBuff : PowerupEffects
{
    public int amount = 1;
    public override void ApplyPowerup()
    {
        GameManager.Instance.playerController.GetHpBuff(amount);
    }
}
