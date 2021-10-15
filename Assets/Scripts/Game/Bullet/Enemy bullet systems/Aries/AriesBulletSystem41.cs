using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem41 : EnemyBulletSubsystem
{
    Vector2 RandomSpawnPos => new Vector2(Random.Range(-8f, 8f), 6f);

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = -45; i < 45; i++)
            {
                SpawnProjectile(6, i, RandomSpawnPos, false).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }

            for (int i = 45; i > -45; i--)
            {
                SpawnProjectile(6, i, RandomSpawnPos, false).Fire();
                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}