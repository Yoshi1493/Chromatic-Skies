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
                    int z = (i * 45) + (j * 120);
                    SpawnBullet(0, z, transform.up.RotateVectorBy(z));
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(ShootingCooldown * 2f);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    int z = j * 12 + (i * 6);
                    SpawnBullet(1, z, Vector2.zero);
                }

                yield return WaitForSeconds(ShootingCooldown * 5f);
            }

            yield return ownerShip.MoveToRandomPosition(1f, 1f);
        }
    }
}