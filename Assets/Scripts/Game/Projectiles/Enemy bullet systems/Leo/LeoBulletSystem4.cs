using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int RepeatCount = 6;
    const int WaveCount = 5;
    const float WaveSpacing = 360f / WaveCount;
    const int BulletCount = 12;
    const float BulletSpawnRadius = 1.5f;

    Queue<EnemyBullet> bullets = new(RepeatCount * WaveCount * BulletCount);

    protected override float ShootingCooldown => 1f / 60;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            //SetSubsystemEnabled(1);

            for (int i = 0; i < RepeatCount; i++)
            {
                Vector3 v0 = ownerShip.transform.position;
                StartMoveAction?.Invoke();

                for (int ii = 0; ii < WaveCount; ii++)
                {
                    Vector3 v1 = Vector3.up.RotateVectorBy(2 * ii * WaveSpacing);
                    Vector3 v2 = Vector3.up.RotateVectorBy(2 * (ii + 1) * WaveSpacing);

                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = 0f;
                        Vector3 pos = BulletSpawnRadius * Vector3.Lerp(v1, v2, (float)iii / BulletCount) + v0;

                        bulletData.colour = bulletData.gradient.Evaluate(iii / (BulletCount - 1f));

                        SpawnProjectile(0, z, pos, false).Fire();
                        yield return WaitForSeconds(ShootingCooldown);
                    }
                }

                yield return WaitForSeconds(0.5f);
            }

            yield return WaitForSeconds(10f);
        }
    }

}