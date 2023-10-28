using System.Collections;
using UnityEngine;

public class CancerBulletSystem5 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        //SetSubsystemEnabled(1);

        float z = 0f;
        Vector3 pos = Vector3.zero;

        SpawnProjectile(0, z, pos).Fire();
    }
}