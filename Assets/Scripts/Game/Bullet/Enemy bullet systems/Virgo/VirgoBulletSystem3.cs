using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem3 : EnemyLaserSystem
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        for (int i = 0; i < 32; i++)
        {
            float z = Mathf.PingPong(i, 8) * 10f - 40f + (i / 8 * 5 - 7.5f);
            SpawnProjectile(0, z, Vector3.zero).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}