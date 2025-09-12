using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoShooter01 : CommonEnemyShooter<EnemyBullet>
{
    const float RepeatCount = 2;
    const int WaveCount = 17;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpacing = 12f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        for (int i = 0; i < RepeatCount; i++)
        {
            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    int bulletCount = (int)Mathf.PingPong(ii, WaveCount / 2) + 1;

                    for (int iv = 0; iv < bulletCount; iv++)
                    {
                        float z = ((iv - (bulletCount - 1) / 2f) * BulletSpacing) + (iii * BranchSpacing);
                        Vector3 pos = Vector3.zero;

                        bulletData.colour = bulletData.gradient.Evaluate(ii / (WaveCount - 1f));

                        SpawnProjectile(1, z, pos).Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(3f);
        }
    }
}