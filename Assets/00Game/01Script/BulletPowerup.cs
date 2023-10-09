using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Bullet")]
public class BulletPowerup : PowerupEffects
{
    public int bulletNumbers = 3;
    public override void ApplyPowerup(GameObject target)
    {
        target.GetComponent<ShootAbility>().bulletCount += bulletNumbers;
    }
}
