using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem2 : EnemyBulletSystem
{
    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return base.Shoot();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 45; j++)
                {
                    float z = (j * 8f) + (i * 4f);

                    SpawnBullet(3, z, Vector2.zero).Fire();
                    SpawnBullet(4, z, Vector2.zero).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown * 8f);
            }

            yield return ownerShip.MoveToRandomPosition(1f, 1f);
        }
    }
}