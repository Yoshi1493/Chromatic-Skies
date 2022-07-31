using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem2 : EnemyShooter<EnemyBullet>
{
    readonly float Spacing = 15f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 5; i <= 9; i += 2)
            {
                float theta = PlayerPosition.GetRotationDifference(transform.position);

                for (int j = 0; j < i; j++)
                {
                    int bulletCount = (int)Mathf.PingPong(j, i / 2) + 1;
                    float offset = ((bulletCount - 1) * Spacing / 2f);

                    for (int k = 0; k < bulletCount; k++)
                    {
                        float z = theta + (k * Spacing - offset);
                        Vector3 pos = Vector3.zero;

                        SpawnProjectile(0, z, pos).Fire();
                    }

                    yield return WaitForSeconds(ShootingCooldown);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(ShootingCooldown * 2f);

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 360; j += 18)
                {
                    float z = j + (i * 2f);
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(1, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(ShootingCooldown * 4f);

            for (int i = 9; i > 0; i--)
            {
                for (int j = 0; j < 360; j += 18)
                {
                    float z = j + (i * 3f);
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(1, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(1f, maxSqrMagDelta: 3f, delay: 1f);
        }
    }
}