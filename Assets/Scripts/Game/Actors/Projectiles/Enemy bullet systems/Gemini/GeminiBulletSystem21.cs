using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 48;
    const float WaveSpacing = 0.25f;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 3;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.2f;

    List<(Vector2 pos, float z)> bulletSpawnData = new(WaveCount * BranchCount);

    protected override float ShootingCooldown => 1f / 60;

    protected override IEnumerator Shoot()
    {
        bulletSpawnData.Clear();

        for (int i = 1; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = 0f;
                Vector3 pos = i * WaveSpacing * transform.up.RotateVectorBy(ii * BranchSpacing + 90f);

                SpawnProjectile(2, z, pos).Fire();
                bulletSpawnData.Add((pos, z));
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        yield return WaitForSeconds(2f);

        bulletSpawnData.Randomize();

        for (int i = 1; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                int b = BranchCount * (i - 1) + ii;

                for (int iii = 0; iii < BulletCount; iii++)
                {
                    var (pos, z) = bulletSpawnData[b];
                    float s = BulletBaseSpeed + (iii * BulletSpeedModifier);

                    bulletData.colour = bulletData.gradient.Evaluate(iii / (BulletCount - 1f));

                    var bullet = SpawnProjectile(3, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown * 3f);
        }

        enabled = false;
    }
}