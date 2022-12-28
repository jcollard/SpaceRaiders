using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPowerUpController : PowerUpController
{
    [field: SerializeField]
    public GameObject Weapon { get; private set; }

    public override void Collect(PlayerController player)
    {
        player.Laser = Weapon;
        player.ShieldPower++;
        base.Collect(player);
    }
}
