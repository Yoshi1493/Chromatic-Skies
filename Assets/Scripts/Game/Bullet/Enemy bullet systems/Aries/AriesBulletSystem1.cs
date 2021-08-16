using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem1 : EnemyBulletSystem
{
    const float RotationAmount = 45;
    const float StaggerAmount = 120;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        for (int i = 0; i < 48; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                float z = (i * RotationAmount) + (j * StaggerAmount);
                SpawnBullet(0, z, transform.up.RotateVectorBy(z));
            }

            yield return WaitForSeconds(ShootingCooldown * 2);
        }

        //to-do: move to random position

        enabled = false;
    }
}