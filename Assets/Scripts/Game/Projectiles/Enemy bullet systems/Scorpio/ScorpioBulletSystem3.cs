using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 5;
    const float WaveSpacing = 2f;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    
    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float x = (ii % 2 * 2 - 1) * (1.1f * screenHalfWidth);
                float y = (0.9f * screenHalfHeight) - ((i + (ii * 0.5f)) * WaveSpacing);
                Vector3 pos = new(x, y);

                float z = -90f * Mathf.Sign(pos.x);

                SpawnProjectile(0, z, pos, false).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}