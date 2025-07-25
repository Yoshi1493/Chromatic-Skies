using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class AriesShooter01 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 5;
    const float BranchSpacing = 5f;
    const int BulletCount = 3;
    const float BulletBaseSpeed = 4f;
    const float BulletSpeedModifier = 0.6f;

    List<EnemyBullet> bullets = new(BranchCount * BulletCount);

    protected override IEnumerator Shoot()
    {
        bullets.Clear();

        yield return WaitForSeconds(1f);

        for (int i = 0; i < BranchCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = PlayerPosition.GetRotationDifference(transform.position);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(ii / (BulletCount - 1f));

                var bullet = SpawnProjectile(1, z, pos);
                bullet.StartCoroutine(bullet.LerpSpeed(BulletBaseSpeed * 2f, 0f, 0.5f));
                bullets.Add(bullet);
            }
        }

        yield return WaitForSeconds(0.5f);

        for (int i = 0; i < BranchCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                int b = (i * BulletCount) + ii;

                if (bullets[b].isActiveAndEnabled)
                {
                    float z = (i - ((BranchCount - 1) / 2)) * BranchSpacing;
                    float s = BulletBaseSpeed + (ii * BulletSpeedModifier);

                    bullets[b].StartCoroutine(bullets[b].RotateBy(z, 0.5f));
                    bullets[b].StartCoroutine(bullets[b].LerpSpeed(0f, s, 1f));
                }
            }
        }

        yield return WaitForSeconds(ShootingCooldown);

    }
}