using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem1 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    float z = j * 72f;

                    SpawnProjectile(0, z, Vector3.zero).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(1f);
        }
    }
}