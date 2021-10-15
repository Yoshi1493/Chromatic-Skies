using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem1 : EnemyBulletSystem
{
    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return base.Shoot();

            for (int i = 0; i < 48; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    float z = (i * 45f) + (j * 120f);
                    SpawnProjectile(1, z, Vector2.zero).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(ShootingCooldown * 5f);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    float z = j * 12f + (i * 6f);
                    SpawnProjectile(0, z, Vector2.zero).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown * 5f);
            }

            yield return ownerShip.MoveToRandomPosition(1f, 1f);
        }
    }
}