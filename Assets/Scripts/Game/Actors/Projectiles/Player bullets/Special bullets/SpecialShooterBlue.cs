using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialShooterBlue : PlayerSpecialShooter
{
    protected override float SpecialCooldown => 8f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        yield return WaitForSeconds(ShootingCooldown);
        canShoot = true;
    }
}