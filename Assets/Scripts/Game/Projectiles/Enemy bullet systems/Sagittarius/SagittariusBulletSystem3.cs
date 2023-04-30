using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 30;
    const float WaveSpacing = 1f;
    const int BranchCount = 10;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletBaseSpeed = 3f;
    const float BulletSpeedMultiplier = 0.2f;
    const float BulletSpawnRadius = 0.5f;

    protected override float ShootingCooldown => 0.04f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        int r = 1;

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = ((i * WaveSpacing) + (ii * BranchSpacing)) * r;
                    float s = BulletBaseSpeed + (i * BulletSpeedMultiplier);
                    Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(z + (90f * r));

                    bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(ShootingCooldown * 5f);
            r *= -1;

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = ((i * WaveSpacing) + (ii * BranchSpacing)) * r + (BranchSpacing / 2f);
                    float s = BulletBaseSpeed + (i * BulletSpeedMultiplier);
                    Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(z + (90f * r));

                    bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            for (int i = 0; i < 3; i++)
            {
                yield return WaitForSeconds(1f);

                StartMoveAction?.Invoke();
                SetSubsystemEnabled(1);
            }

            yield return WaitForSeconds(1f);
        }
    }
}