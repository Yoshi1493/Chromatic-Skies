using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem3 : EnemyShooter<EnemyBullet>
{
    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        Transform c = transform.GetChild(0);

        while (enabled)
        {
            yield return WaitForSeconds(ShootingCooldown);

            float z = c.eulerAngles.z;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(0, z, pos).Fire();
        }
    }
}