using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem41 : EnemyBulletSubsystem<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        while (enabled)
        {
            int rand = Random.Range(1, 5);

            for (int i = 0; i < rand; i++)
            {
                float x = Random.Range(-8f, 8f);
                Vector3 spawnPos = new Vector3(x, 6f);

                SpawnProjectile(1, 0f, spawnPos, false).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown * 3);
        }
    }
}