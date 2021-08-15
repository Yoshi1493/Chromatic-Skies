using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem2 : EnemyBulletSystem
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        float rotationAmount = 12f;

        for (int i = 0; i < 120; i++)
        {
            float z = i * rotationAmount;
            SpawnBullet(0, z, transform.up.RotateVectorBy(z));
            z += 186;
            SpawnBullet(0, z, transform.up.RotateVectorBy(z));

            yield return WaitForSeconds(ShootingCooldown);
        }

        //to-do: move to random position

        enabled = false;
    }
}