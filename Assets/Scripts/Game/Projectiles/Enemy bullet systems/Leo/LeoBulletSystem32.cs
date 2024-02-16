using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem32 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 2;
    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float x = 0.8f * Random.Range(-screenHalfWidth, screenHalfWidth);
                float y = screenHalfHeight * Random.Range(1.2f, 1.5f);
                Vector3 pos = new(x, y);
                float z = 0f;

                SpawnProjectile(1, z, pos, false).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}