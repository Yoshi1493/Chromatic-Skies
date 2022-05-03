using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class AriesBulletSystem4 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            yield return WaitForSeconds(2f);

            int randDirection = PositiveOrNegativeOne;
            float n = 0f;
            float t = Random.Range(0f, 90f);

            for (int i = 0; i < 360; i += 4)
            {
                for (int j = 0; j < 5; j++)
                {
                    float z = j * 72f + n + t;
                    SpawnProjectile(0, z, Vector2.zero).Fire();
                }

                n += i * randDirection;
                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(1f, delay: 1f);
        }
    }
}