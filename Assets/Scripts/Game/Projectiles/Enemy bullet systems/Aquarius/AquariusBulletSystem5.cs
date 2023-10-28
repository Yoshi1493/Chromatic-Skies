using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem5 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        float z = 0;
        Vector3 pos = Vector3.zero;

        SpawnProjectile(0, z, pos).Fire();

        yield return WaitForSeconds(2f);

        SetSubsystemEnabled(1);
    }
}