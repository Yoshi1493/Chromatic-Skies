using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 4;
    const float WaveSpacing = BranchSpacing / 2f;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 3;
    const float BulletSpacing = 1f;
    const int BulletsPerBranch = 6;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 1f;
    const float BulletRotationSpeed = 12f;
    const float BulletRotationSpeedModifier = 12f;

    List<EnemyBullet> bullets = new(BranchCount * BulletCount * BulletsPerBranch);

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
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

                            var bullet = SpawnProjectile(0, z, pos);
                            bullet.StartCoroutine(bullet.LerpSpeed(s, 0f, 1f));
                            bullets.Add(bullet);
                        }
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        for (int iv = 0; iv < BulletsPerBranch; iv++)
                        {
                            int b = (ii * BulletCount * BulletsPerBranch) + (iii * BulletsPerBranch) + iv;
                            float r = BulletRotationSpeed + (b * BulletRotationSpeedModifier);

                            if (bullets[b].isActiveAndEnabled)
                            {
                                bullets[b].StopAllCoroutines();
                                bullets[b].StartCoroutine(bullets[b].RotateBy(r, 0f));
                                bullets[b].Fire();
                            }
                        }
                    }
                }

                bullets.Clear();
            }

            yield return WaitForSeconds(1f);

            StartMoveAction?.Invoke();
            SetSubsystemEnabled(1);

            yield return WaitForSeconds(2f);
        }
    }
}