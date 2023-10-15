using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CapricornBulletSystem51 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 36;
    const int BranchCount = 1;

    protected override float ShootingCooldown => 0.08f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = RandomAngleDeg;
                Vector3 pos = Vector3.zero;

                SpawnProjectile(1, z, pos).Fire();
                yield return WaitForSeconds(ShootingCooldown);
                SpawnProjectile(2, z + 180f, pos).Fire();
            }
        }

        enabled = false;
    }
}