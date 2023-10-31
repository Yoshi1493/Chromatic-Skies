using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class AriesBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 90;
    const int BranchCount = 5;
    const float BranchSpacing = 360 / BranchCount;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        StartMoveAction?.Invoke();
        SetSubsystemEnabled(1);

        int i = 0;
        float r = RandomAngleDeg;

        for (int ii = 1; enabled; ii++)
        {
            for (int iii = 0; iii < BranchCount; iii++)
            {
                float z = iii * BranchSpacing + i + r;
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(Mathf.PingPong(ii, WaveCount) / (WaveCount - 1));
                SpawnProjectile(0, z, pos).Fire();
            }

            i = (i + ii) % 360;
            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}