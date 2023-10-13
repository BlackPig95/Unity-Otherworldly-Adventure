using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Speed")]
public class SpeedBuff : PowerupEffects
{
    public int multiplier = 2;
    public float buffTime = 5.0f;
    public override void ApplyPowerup()
    {
        GameManager.Instance.playerController.GetSpeedBuff(multiplier, buffTime);
    }
}
