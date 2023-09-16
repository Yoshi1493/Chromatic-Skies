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

        while (enabled)
        {
            SetSubsystemEnabled(1);
            yield return WaitForSeconds(2f);

            for (int i = 0; i < WaveCount; i++)
            {
                float r = PlayerPosition.GetRotationDifference(transform.position);
                int maxBulletCount = (i * 2) + 5;

                for (int ii = 0; ii < maxBulletCount; ii++)
                {
                    int currentBulletCount = (int)Mathf.PingPong(ii, maxBulletCount / 2) + 1;
                    float t = (currentBulletCount - 1) * BulletSpacing / 2f;
                    bulletData.colour = bulletData.gradient.Evaluate(ii / (maxBulletCount - 1f));

                    for (int iii = 0; iii < currentBulletCount; iii++)
                    {
                        for (int iv = 0; iv < BranchCount; iv++)
                        {
                            float z = (iii * BulletSpacing) + (iv * BranchSpacing) + r - t;
                            Vector3 pos = Vector3.zero;

                            SpawnProjectile(0, z, pos).Fire();
                        }
                    }

                    yield return WaitForSeconds(ShootingCooldown);
                }

                yield return WaitForSeconds(ShootingCooldown * 3f);
            }

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(1f);
        }
    }
}