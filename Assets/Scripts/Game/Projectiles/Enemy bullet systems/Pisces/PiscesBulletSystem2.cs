using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpacing = 15f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                float r = PlayerPosition.GetRotationDifference(transform.position);
                int m = (i * 2) + 5;

                for (int ii = 0; ii < m; ii++)
                {
                    int n = (int)Mathf.PingPong(ii, m / 2) + 1;
                    float o = (n - 1) * BulletSpacing / 2f;
                    bulletData.colour = bulletData.gradient.Evaluate(ii / (m - 1f));

                    for (int iii = 0; iii < n; iii++)
                    {
                        for (int iv = 0; iv < BranchCount; iv++)
                        {
                            float z = (iii * BulletSpacing) + (iv * BranchSpacing) + r - o;
                            Vector3 pos = Vector3.zero;

                            SpawnProjectile(0, z, pos).Fire();
                        }
                    }

                    yield return WaitForSeconds(ShootingCooldown);
                }

                yield return WaitForSeconds(ShootingCooldown * 3f);
            }

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(2f);
        }
    }
}