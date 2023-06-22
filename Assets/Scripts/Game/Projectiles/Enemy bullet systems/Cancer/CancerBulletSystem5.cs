using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 22;
    const float WaveSpacing = -6f;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletBaseSpeed = 4f;
    const float BulletSpeedMultiplier = 0.1f;

    protected override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 60;

    }

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing);
                    float s = BulletBaseSpeed + (i * BulletSpeedMultiplier);
                    Vector3 pos = 2f * Vector3.forward;

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }
    }
}