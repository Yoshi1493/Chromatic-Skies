using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialShooterGreen : PlayerSpecialShooter
{
    protected override float SpecialCooldown => 10f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        yield return WaitForSeconds(SpecialCooldown);
        canShoot = true;
    }
}