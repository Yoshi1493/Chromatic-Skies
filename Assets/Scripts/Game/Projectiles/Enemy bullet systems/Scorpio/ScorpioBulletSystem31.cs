using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 15f;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 3;
    const float BulletSpacing = 8f;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        for (int i = 0; enabled; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float t = (i * WaveSpacing) + ((ii + 0.5f) * BranchSpacing);
                float r = PlayerPosition.GetRotationDifference(transform.position);

                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = ((iii - ((BulletCount - 1) / 2f)) * BulletSpacing) + r;
                    Vector3 pos = transform.up.RotateVectorBy(t);

                    bulletData.colour = bulletData.gradient.Evaluate(ii);

                    SpawnProjectile(2, z, pos).Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}