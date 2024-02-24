using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 6;
    const int BranchCount = 5;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 12;
    const float BulletSpacing = 6f;
    const float BulletSpawnRadius = 1.5f;
    const float SpawnRadiusModifier = 0.15f;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.15f;

    Queue<EnemyBullet> bullets = new(WaveCount * BranchCount * BulletCount);

    protected override float ShootingCooldown => 1f / 60;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            //SetSubsystemEnabled(1);

            for (int i = 0; i < WaveCount; i++)
            {
                Vector3 v0 = ownerShip.transform.position;
                StartMoveAction?.Invoke();

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    Vector3 v1 = Vector3.up.RotateVectorBy(2 * ii * BranchSpacing);
                    Vector3 v2 = Vector3.up.RotateVectorBy(2 * (ii + 1) * BranchSpacing);

                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        Vector3 pos = (BulletSpawnRadius + (i * SpawnRadiusModifier))* Vector3.Lerp(v1, v2, (float)iii / BulletCount) + v0;
                        float z = v0.GetRotationDifference(pos) + (iii * BulletSpacing);

                        bulletData.colour = bulletData.gradient.Evaluate(iii / (BulletCount - 1f));

                        bullets.Enqueue(SpawnProjectile(0, z, pos, false));
                        yield return WaitForSeconds(ShootingCooldown);
                    }
                }

                StartCoroutine(FireBullets());
            }

            yield return WaitForSeconds(10f);
        }
    }

    IEnumerator FireBullets()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < BranchCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float s = BulletBaseSpeed + (i * BulletSpeedModifier);

                if (bullets.TryDequeue(out EnemyBullet bullet))
                {
                    bullet.StartCoroutine(bullet.LerpSpeed(0f, s, 1f));
                }
            }
        }
    }
}