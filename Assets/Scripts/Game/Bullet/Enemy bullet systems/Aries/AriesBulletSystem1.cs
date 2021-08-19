using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem1 : EnemyBulletSystem
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        for (int i = 0; i < 48; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                float z = (i * 45) + (j * 120);
                SpawnBullet(0, z, transform.up.RotateVectorBy(z));
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        yield return WaitForSeconds(ShootingCooldown * 2);

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                float z = j * 12 + (i * 6);
                SpawnBullet(1, z, Vector2.zero);
            }

            yield return WaitForSeconds(ShootingCooldown * 5);
        }

        yield return ownerShip.MoveToRandomPosition(1, 2);

        OnEnable();
    }
}