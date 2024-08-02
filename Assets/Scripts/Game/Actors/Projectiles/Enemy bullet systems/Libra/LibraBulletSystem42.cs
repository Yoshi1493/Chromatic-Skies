using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem42 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 40;
    const float WaveSpacing = 15f;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 1;
    const float BulletSpawnRadius = 0.2f;
    const float SpawnRadiusModifier = 0.02f;
    const float BulletBaseSpeed = 1.5f;
    const float BulletSpeedModifier = 0.05f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                float r = PlayerPosition.GetRotationDifference(transform.position);

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float t = (iii % 2 * 2 - 1) * (i * WaveSpacing);
                        float z = (ii * BranchSpacing) + r;
                        float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                        Vector3 pos = (BulletSpawnRadius + (i * SpawnRadiusModifier)) * transform.up.RotateVectorBy(t);

                        bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                        var bullet = SpawnProjectile(3, z, pos);
                        bullet.StartCoroutine(bullet.LerpSpeed(5f, s, 1f));
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            enabled = false;
        }
    }
}