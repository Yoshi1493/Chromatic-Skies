using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const float WaveSpacing = 360f / WaveCount;
    const int BaseRingCount = 4;
    const float RingSpacing = 16f;
    const int BulletCount = 16;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 3f;
    const float BulletSpeedModifier = 1f;
    const float BulletRotationSpeed = 30f;

    List<EnemyBullet> bullets = new(BaseRingCount * BulletCount);

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            bullets.Clear();

            int ringCount = (i + 1) * BaseRingCount;

            for (int ii = 0; ii < ringCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (i % 2 * 2 - 1) * ((ii * RingSpacing) + (iii * BulletSpacing));
                    float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(ii / (ringCount - 1f));

                    var bullet = SpawnProjectile(2, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(s, 0f, 0.5f));                    
                    bullets.Add(bullet);
                }
            }

            yield return WaitForSeconds(ShootingCooldown);

            for (int ii = 0; ii < ringCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    int b = (ii * BulletCount) + iii;
                    float r = (ii % 2 * 2 - 1) * BulletRotationSpeed;

                    bullets[b].Fire();
                    bullets[b].StartCoroutine(bullets[b].RotateBy(r, 1f));
                }
            }
        }

        enabled = false;
    }
}