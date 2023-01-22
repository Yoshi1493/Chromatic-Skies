using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 11;
    const float WaveSpacing = 360f / (2 * (WaveCount - 1));
    const int BulletCount = 8;
    const float BulletSpeed = 1.5f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            yield return WaitForSeconds(2f);

            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int i = 0; i < WaveCount; i++)
            {
                int BranchCount = i % (WaveCount - 1) == 0 ? 1 : 2;

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (i * WaveSpacing) * (-ii * 2 + 1) + r;
                    Vector3 pos = Vector3.zero;

                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float s = BulletSpeed + (iii * 0.5f);
                        bulletData.colour = bulletData.gradient.Evaluate(iii / (BulletCount - 1f));

                        var bullet = SpawnProjectile(0, z, pos);
                        bullet.MoveSpeed = s;
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(2f);
        }
    }
}