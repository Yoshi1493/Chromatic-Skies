using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem31 : BossShooter<BossBullet>
{
    const int WaveCount = 9;
    const float WaveSpacing = 12f;
    const int BranchCount = 2;
    const int BulletCount = 6;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.3f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(4f);

            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = (ii % 2 * 2 - 1) * (i * WaveSpacing) + r;
                        float s = BulletBaseSpeed + (iii * BulletSpeedModifier);
                        Vector3 pos = Vector3.zero;

                        bulletData.colour = bulletData.gradient.Evaluate(iii / (BulletCount - 1f));

                        var bullet = SpawnProjectile(1, z, pos);
                        bullet.MoveSpeed = s;
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }

}