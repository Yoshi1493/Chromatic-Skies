using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 24;
    const float WaveSpacing = 360f / WaveCount;
    const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;

    protected override float ShootingCooldown => 0.08f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            yield return WaitForSeconds(1f);

            for (int i = 0; i < WaveCount; i++)
            {
                float s = (WaveCount * 2 - i) * 0.1f;

                for (int j = 0; j < BranchCount; j++)
                {
                    float z = (i * WaveSpacing) + (j * BranchSpacing);
                    Vector3 pos = transform.up.RotateVectorBy(z);

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.Fire();

                    pos = transform.up.RotateVectorBy(-z);

                    bullet = SpawnProjectile(1, -z, pos);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            SetSubsystemEnabled(1);

            yield return ownerShip.MoveToRandomPosition(1f, delay: 1f);
        }
    }
}