using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const int RingCount = 20;
    const float RingSpacing = 3f;
    const int BulletCount = 16;
    const float BranchSpacing = 360f / BulletCount;
    const float BulletSpacing = 0.25f;

    Stack<EnemyBullet> bullets = new(RingCount * BulletCount);

    protected override float ShootingCooldown => 0.06f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < RingCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = -(i * RingSpacing) - (ii * BranchSpacing);
                    Vector3 pos = i * BulletSpacing * transform.up.RotateVectorBy(z);
                    bulletData.colour = bulletData.gradient.Evaluate(i / (RingCount - 1f));

                    bullets.Push(SpawnProjectile(1, z, pos));
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            for (int i = 0; i < RingCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    var b = bullets.Pop();

                    if (b.enabled)
                    {
                        b.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown * 2f);
            }

            bullets.Clear();

            enabled = false;
        }
    }
}