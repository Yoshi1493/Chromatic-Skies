using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem3 : EnemyShooter<EnemyBullet>
{
    protected override float ShootingCooldown => 2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        Transform c = transform.GetChild(0);
        yield return WaitForSeconds(1f);

        while (enabled)
        {
            float z = c.eulerAngles.z;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(0, z, pos).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}