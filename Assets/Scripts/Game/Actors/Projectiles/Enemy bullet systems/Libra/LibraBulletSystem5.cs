using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 2;
    const float WaveSpacing = 3f;
    const int BranchCount = 24;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 12;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.4f;
    const float BulletRotationSpeed = 30;
    const float BulletRotationModifier = 6f;

    List<EnemyBullet> bullets = new(BranchCount * BulletCount);

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                bullets.Clear();

                int d = i % 2 * 2 - 1;

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = d * ((i * WaveSpacing) + (ii * BranchSpacing));
                        Vector3 pos = Vector3.zero;

                        bulletData.colour = bulletData.gradient.Evaluate(iii / (BulletCount - 1f));

                        var bullet = SpawnProjectile(0, z, pos);
                        bullet.StartCoroutine(bullet.LerpSpeed(3f, 0f, 1f));
                        bullets.Add(bullet);
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        int b = (ii * BulletCount) + iii;
                        float s = BulletBaseSpeed + (iii * BulletSpeedModifier);
                        float r = d * (BulletRotationSpeed + (iii * BulletRotationModifier));

                        bullets[b].StartCoroutine(bullets[b].LerpSpeed(0f, s, 1f));
                        bullets[b].StartCoroutine(bullets[b].RotateBy(r, 1f));
                        bullets[b].Fire();
                    }
                }
            }

            yield return WaitForSeconds(1f);

            StartMoveAction?.Invoke();
            SetSubsystemEnabled(1);

            yield return WaitForSeconds(7f);
        }
    }
}