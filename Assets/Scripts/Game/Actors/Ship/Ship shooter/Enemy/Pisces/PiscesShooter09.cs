using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class PiscesShooter09 : CommonEnemyShooter<EnemyBullet>
{
    const int RepeatCount = 2;
    const int WaveCount = 45;
    const float WaveSpacing = 180f / WaveCount;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnRadius = 1f;
    const float SpawnRadiusModifier = -0.015f;
    const float BulletClumpCount = 3;
    const float BulletRotationSpeed = WaveSpacing / 2f;
    const float BulletRotationDuration = 2f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        int d = PositiveOrNegativeOne;

        for (int i = 0; i < RepeatCount; i++)
        {
            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float z = d * ((ii * WaveSpacing) + (iii * BranchSpacing) + 90f);
                    float r = d * ((ii % BulletClumpCount) - ((BulletClumpCount - 1) / 2f)) * BulletRotationSpeed;
                    Vector3 pos = (BulletSpawnRadius + (ii * SpawnRadiusModifier)) * transform.up.RotateVectorBy(z);

                    bulletData.colour = bulletData.gradient.Evaluate(ii / (WaveCount - 1f));

                    var bullet = SpawnProjectile(8, z, pos);
                    bullet.StartCoroutine(bullet.RotateBy(r, BulletRotationDuration, delay: 1f));
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(2f);
            d *= -1;
        }
    }
}