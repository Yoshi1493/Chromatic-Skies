using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem1 : EnemyBulletSystem
{
    float rotationAmount = 48;
    float staggerAmount = 120;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                float z = (i * rotationAmount) + (j * staggerAmount);
                SpawnBullet(0, z, transform.up.RotateVectorBy(z));
            }

            yield return WaitForSeconds(ShootingCooldown * 2);
        }

        for (int i = 0; i < 15; i++)
        {
            float z = (i * 24) + 12;
            SpawnBullet(0, z, transform.up.RotateVectorBy(z));
        }

        //to-do: move to random position

        enabled = false;
    }
}