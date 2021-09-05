using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem3 : EnemyBulletSystem
{
    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return base.Shoot();

            Vector3 distanceFromPlayer = PlayerPosition - ShipPosition;

            for (int i = 0; i < 20; i++)
            {
                float z = i * -18f + 90f;
                SpawnBullet(4, z, distanceFromPlayer + (transform.up.RotateVectorBy(z + 90f) * 3f));

                yield return WaitForSeconds(ShootingCooldown / 10f);
            }

            yield return WaitForSeconds(ShootingCooldown);

            for (int i = 0; i < 16; i++)
            {
                float z = i * -22.5f + 90f;
                SpawnBullet(4, z, distanceFromPlayer + (transform.up.RotateVectorBy(z + 90f) * 2.5f));

                yield return WaitForSeconds(ShootingCooldown / 10f);
            }

            yield return WaitForSeconds(ShootingCooldown);

            for (int i = 0; i < 12; i++)
            {
                float z = i * -30f + 90f;
                SpawnBullet(4, z, distanceFromPlayer + (transform.up.RotateVectorBy(z + 90f) * 2f));

                yield return WaitForSeconds(ShootingCooldown / 10f);
            }

            //for (int i = 0; i < 16; i++)
            //{
            //    float z = i * 22.5f - 90f;
            //    SpawnBullet(5, z, distanceFromPlayer + (transform.up.RotateVectorBy(z - 90f) * 2.5f));

            //    yield return WaitForSeconds(ShootingCooldown / 10f);
            //}

            yield return WaitForSeconds(1f);

            for (int i = 0; i < 16; i++)
            {
                float rand = Random.Range(-90f, 90f);

                for (int j = 0; j <= 5; j++)
                {
                    float z = rand + (j * 30f) - 120f;
                    SpawnBullet(1, z, transform.up.RotateVectorBy(rand));
                }

                yield return WaitForSeconds(ShootingCooldown * 2.5f);
            }

            yield return WaitForSeconds(10f);

            yield return ownerShip.MoveToRandomPosition(1f, 1f);
        }
    }
}