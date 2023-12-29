using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int RepeatCount = 4;
    const int WaveCount = (360 - (int)SafeZone) / (int)WaveSpacing + 1;
    const float WaveSpacing = 10f;
    const float SafeZone = 40f;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = 90f;
    const float BulletSpawnRadius = 0.5f;
    const float SpawnRadiusModifier = 0.5f;
    const float BulletRotationSpeed = 45f;

    protected override float ShootingCooldown => 0.02f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < RepeatCount; i++)
            {
                StartMoveAction?.Invoke();

                for (int ii = 0; ii < WaveCount; ii++)
                {
                    for (int iii = 0; iii < BranchCount; iii++)
                    {
                        float r = i % 2 * 2 - 1;
                        float z = r * ((SafeZone / 2f) + (ii * WaveSpacing) + (iii * BranchSpacing));
                        Vector3 pos = (BulletSpawnRadius + (iii * SpawnRadiusModifier)) * transform.up.RotateVectorBy(z);

                        for (int iv = 0; iv < BulletCount; iv++)
                        {
                            z += iv * BulletSpacing;

                            bulletData.colour = bulletData.gradient.Evaluate(iv);

                            var bullet = SpawnProjectile(0, z, pos);
                            bullet.Fire();
                            bullet.StartCoroutine(bullet.RotateBy(r * BulletRotationSpeed, 0f, delay: 1f));
                        }
                    }

                    yield return WaitForSeconds(ShootingCooldown);
                }

                yield return WaitForSeconds(0.5f);
            }

            SetSubsystemEnabled(1);
            yield return WaitForSeconds(3f);
        }
    }
}