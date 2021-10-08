using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem21 : EnemyBulletSubsystem
{
    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                float z = (j * 12f) + (i * 6f);

                SpawnBullet(3, z, Vector2.zero).Fire();
                SpawnBullet(4, z, Vector2.zero).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown * 8f);
        }

        enabled = false;
    }
}