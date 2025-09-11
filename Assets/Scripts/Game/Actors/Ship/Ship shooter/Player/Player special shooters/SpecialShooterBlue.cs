using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialShooterBlue : PlayerSpecialShooter
{
    protected override float SpecialCooldown => 8f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        float z = 0f;
        Vector3 pos = Vector3.zero;

        SpawnProjectile(0, z, pos).Fire();

        yield return WaitForSeconds(SpecialCooldown);
        CanShoot = true;
    }
}