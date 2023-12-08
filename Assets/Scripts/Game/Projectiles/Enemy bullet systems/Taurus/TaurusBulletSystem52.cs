using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem52 : EnemyShooter<Laser>
{
    const int LaserCount = 2;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        for (int i = 0; i < LaserCount; i++)
        {
            float z = 0;
            Vector3 pos = Vector3.zero;

            SpawnProjectile(1, z, pos).Fire(0.2f);
        }

        enabled = false;
    }
}