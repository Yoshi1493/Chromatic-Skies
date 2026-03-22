using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoShooter05 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 40;
    const float WaveSpacing = 360f / WaveCount;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpawnRadius = 0.5f;
    const float SpawnRadiusModifier = 0.1f;
    const float BulletRotationSpeed = 90f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        for (int i = 1; enabled; i *= -1)
        {
            Vector3 v0 = transform.position;

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float t = i * ii * WaveSpacing;

                    for (int iv = 0; iv < BulletCount; iv++)
                    {
                        float z = t + (iii * BranchSpacing);
                        float r = (iv % 2 * 2 - 1) * BulletRotationSpeed;
                        Vector3 pos = v0 + ((BulletSpawnRadius + (ii * SpawnRadiusModifier)) * transform.up.RotateVectorBy(t));

                        bulletData.colour = bulletData.gradient.Evaluate(iv);

                        var bullet = SpawnProjectile(5, z, pos, false);
                        bullet.StartCoroutine(bullet.RotateBy(r, 0f, delay: 1f));
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(6f);
        }
    }
}