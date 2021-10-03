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

            for (int i = 0; i < 128; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    float z = (i * 50f) + (j * 90f);
                    SpawnBullet(3, z, Vector2.zero).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown / 2f);
            }

            yield return ownerShip.MoveToRandomPosition(1f, 1f);

        }
    }
}