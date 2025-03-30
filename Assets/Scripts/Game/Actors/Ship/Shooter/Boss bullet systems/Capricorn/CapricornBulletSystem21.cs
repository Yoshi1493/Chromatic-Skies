using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CapricornBulletSystem21 : BossShooter<BossBullet>
{
    const int WaveCount = 5;
    const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 5;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.2f;

    protected override float ShootingCooldown => 0.3f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float r = RandomAngleDeg;

                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = ii * BranchSpacing;
                    float s = BulletBaseSpeed + (iii * BulletSpeedModifier);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(iii / (BulletCount - 1f));

                    var bullet = SpawnProjectile(2, z, pos);

                    if (ii == 0)
                    {
                        bullet.StartCoroutine(bullet.GraduallyLookAt(PlayerPosition, 0.1f, delay: 1.2f));
                    }
                    else
                    {
                        bullet.StartCoroutine(bullet.RotateBy(r, 0f, delay: 1.2f));
                    }

                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }

            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}