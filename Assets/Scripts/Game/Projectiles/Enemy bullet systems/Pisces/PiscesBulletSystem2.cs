using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = BranchSpacing / 2f;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 3;
    const float BulletSpacing = 1f;
    const int BulletsPerBranch = 6;
    const float BulletBaseSpeed = 3f;
    const float BulletSpeedModifier = 1.5f;
    const float BulletRotationSpeed = 30f;

    List<EnemyBullet> bullets = new(BranchCount * BulletCount * BulletsPerBranch);

    protected override float ShootingCooldown => 1.5f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        for (int i = 0; enabled; i++)
        {
            bullets.Clear();

            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BranchSpacing);

                for (int iii = 0; iii < BulletCount; iii++)
                {
                    Vector3 pos = ((iii - ((BulletCount - 1) / 2f)) * BulletSpacing * Vector3.right).RotateVectorBy(z);

                    for (int iv = 0; iv < BulletsPerBranch; iv++)
                    {
                        float s = BulletBaseSpeed + (iv * BulletSpeedModifier);

                        bulletData.colour = bulletData.gradient.Evaluate(iv / (BulletsPerBranch - 1f));

                        var bullet = SpawnProjectile(0, z, pos) as PiscesBullet20;
                        bullet.StartCoroutine(bullet.LerpSpeed(s, 0f, 1f));
                        bullet.DestroyAction += OnSpawnedBulletDestroy;
                        bullets.Add(bullet);
                    }
                }
            }

            yield return WaitForSeconds(ShootingCooldown);

            for (int ii = 0; ii < bullets.Count; ii++)
            {
                float r = (ii % 2 * 2 - 1) * BulletRotationSpeed;

                bullets[ii].StartCoroutine(bullets[ii].RotateBy(r, 0f));
                bullets[ii].Fire();
            }
        }
    }

    void OnSpawnedBulletDestroy(EnemyBullet bullet)
    {
        bullets.Remove(bullet);
    }
}