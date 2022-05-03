using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem2 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    float z = (j * 12f) + (i * 6f);

                    SpawnProjectile(0, z, Vector2.zero).Fire();
                    SpawnProjectile(1, z, Vector2.zero).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown * 6f);
            }

            SetSubsystemEnabled(1);

            yield return ownerShip.MoveToRandomPosition(1f, delay: 3f);
            yield return WaitForSeconds(1f);
        }
    }
}