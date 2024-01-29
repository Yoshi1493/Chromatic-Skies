using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class ScorpioBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 44;
    const int BulletCount = 8;
    const float BulletSpawnRadius = 0.8f;
    const float SpawnRadiusModifier = 0.04f;

    List<EnemyBullet> bullets = new(WaveCount * BulletCount);

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            bullets.Clear();

            Vector3 v1 = PlayerPosition;

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = RandomAngleDeg;
                    Vector3 pos = v1 + ((BulletSpawnRadius + (i * SpawnRadiusModifier)) * transform.up.RotateVectorBy(z));

                    var bullet = SpawnProjectile(1, z, pos, false);
                    bullet.Fire();
                    bullets.Add(bullet);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            SpawnProjectile(0, 0f, Vector3.zero).Fire();
            yield return WaitForSeconds(2.5f);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    int b = (i * BulletCount) + ii;

                    if (bullets[b].isActiveAndEnabled)
                    {
                        bullets[b].GetComponent<ITimestoppable>().Resume();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown * 2f);
            }

            yield break;
        }
    }
}