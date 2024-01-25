using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const float WaveSpacing = 360f / WaveCount;
    const int RingCount = 8;
    const float RingSpacing = 15f;
    const int BulletCount = 8;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 1f;
    const float BulletSpeedModifier = 1f;

    List<EnemyBullet> bullets = new(RingCount * BulletCount);

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            bullets.Clear();

            for (int ii = 0; ii < RingCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (i % 2 * 2 - 1) * ((ii * RingSpacing) + (iii * BulletSpacing));
                    float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                    Vector3 pos = Vector3.zero;

                    var bullet = SpawnProjectile(1, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(s, 0f, 0.5f));
                    bullets.Add(bullet);
                }
            }

            yield return WaitForSeconds(ShootingCooldown);

            bullets.ForEach(b => b.Fire());
        }

        enabled = false;
    }
}