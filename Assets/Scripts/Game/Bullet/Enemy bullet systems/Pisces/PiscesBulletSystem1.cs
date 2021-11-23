using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem1 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < 24; i++)
            {
                float z = i * 15f;
                float spd = 2 + (i / 10f);

                var bullet = SpawnProjectile(0, z, Vector3.zero);
                bullet.MoveSpeed = spd;
                bullet.Fire();

                bullet = SpawnProjectile(0, z + 7.5f, Vector3.zero);
                bullet.MoveSpeed = spd * 0.8f;
                bullet.Fire();

                yield return WaitForSeconds(ShootingCooldown / 2f);
            }

            yield return WaitForSeconds(ShootingCooldown);

            for (int i = 0; i < 24; i++)
            {
                float z = (24 - i) * 15f;
                float spd = 2 + (i / 10f);

                var bullet = SpawnProjectile(0, z, Vector3.zero);
                bullet.MoveSpeed = spd;
                bullet.Fire();

                bullet = SpawnProjectile(0, z - 7.5f, Vector3.zero);
                bullet.MoveSpeed = spd * 0.8f;
                bullet.Fire();

                yield return WaitForSeconds(ShootingCooldown / 2f);
            }

            yield return WaitForSeconds(1f);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    float z = j * 12f;
                    SpawnProjectile(1, z, Vector3.zero).Fire();
                }

                yield return ownerShip.MoveToRandomPosition(0.4f + (i / 10f), minSqrMagDelta: 1, maxSqrMagDelta: 2);
            }

            yield return WaitForSeconds(1f);
        }
    }
}