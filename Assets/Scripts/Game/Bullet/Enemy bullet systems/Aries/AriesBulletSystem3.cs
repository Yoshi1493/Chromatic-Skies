using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem3 : EnemyBulletSystem
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        yield return WaitForSeconds(1.5f);

        while (enabled)
        {
            float rand = Random.Range(-90f, 90f);

            for (int j = 0; j <= 6; j++)
            {
                float z = rand + (j * 30f) - 120f;
                SpawnBullet(1, z, transform.up.RotateVectorBy(rand));
            }

            yield return WaitForSeconds(ShootingCooldown * 3f);
        }
    }
}