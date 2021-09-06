using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem31 : EnemyBulletSystem
{
    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return base.Shoot();

            transform.position = PlayerPosition;

            for (int i = 0; i < 12; i++)
            {
                float z = i * -30f + 90f;
                SpawnBullet(4, z, transform.up.RotateVectorBy(z + 90f) * 2f);

                yield return WaitForSeconds(ShootingCooldown / 4f);
            }

            yield return WaitForSeconds(ShootingCooldown);

            for (int i = 0; i < 16; i++)
            {
                float z = i * 22.5f - 90f;
                SpawnBullet(5, z, transform.up.RotateVectorBy(z - 90f) * 2.5f);

                yield return WaitForSeconds(ShootingCooldown / 4f);
            }

            yield return WaitForSeconds(ShootingCooldown);

            while (true)
            {
                transform.Rotate(Vector3.forward, 60 * Time.deltaTime, Space.Self);
                yield return null;
            }

            //for (int i = 0; i < 16; i++)
            //{
            //    float z = i * 22.5f - 90f;
            //    SpawnBullet(4, z, transform.up.RotateVectorBy(z - 90f) * 2.5f);

            //    yield return WaitForSeconds(ShootingCooldown / 10f);
            //}

        }
    }
}