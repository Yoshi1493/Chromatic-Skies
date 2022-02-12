using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem1 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < 72; i++)
            {
                int randCount = Random.Range(2, 5);

                for (int j = 0; j < randCount; j++)
                {
                    float randZ = Random.Range(0f, 360f);
                    SpawnProjectile(0, randZ, Vector3.zero).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown / 2f);
            }

            yield return WaitForSeconds(ShootingCooldown);

            for (int i = 0; i < 6; i++)
            {
                float alt = ((i % 2) - 0.5f) * 2;
                float offset = 10f * alt;

                for (int j = 0; j < 6; j++)
                {
                    for (int k = 0; k < 6; k++)
                    {
                        float z = (offset * (j - 1)) + (k * 60f) + (2.5f * alt);
                        SpawnProjectile(1, z, Vector3.zero).Fire();
                    }

                    yield return WaitForSeconds(ShootingCooldown / 2f);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(1f);
        }
    }
}