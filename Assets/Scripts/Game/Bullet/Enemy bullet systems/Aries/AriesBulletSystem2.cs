using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem2 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return base.Shoot();

            for (int i = 0; i < 111; i++)
            {
                float rand = Random.value * 360;
                for (int j = 0; j < 6; j++)
                {
                    float z = j * 60f + rand;
                    SpawnProjectile(0, z, Vector2.zero).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(3f);
            StartCoroutine(ownerShip.MoveToRandomPosition(1f));

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    float z = j * 30f;

                    SpawnProjectile(1, z, Vector2.zero).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown * 2f);
            }
        }
    }
}