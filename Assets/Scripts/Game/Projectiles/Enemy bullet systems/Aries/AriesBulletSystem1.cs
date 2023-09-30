using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class AriesBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 6;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 6;
    const float BulletSpacing = 10f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        for (int i = 1; enabled;)
        {
            float r = i * BulletSpacing;

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    for (int iv = 0; iv < BulletCount; iv++)
                    {
                        int b = iii % 2;
                        float z = (r * (iii - 1)) + (iv * BranchSpacing);
                        Vector3 pos = Vector3.zero;

                        SpawnProjectile(b, z, pos).Fire();
                    }

                    yield return WaitForSeconds(ShootingCooldown * 0.5f);
                }

                yield return WaitForSeconds(ShootingCooldown);
                i *= -1;
            }

            yield return WaitForSeconds(1f);

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(1f);
        }
    }
}