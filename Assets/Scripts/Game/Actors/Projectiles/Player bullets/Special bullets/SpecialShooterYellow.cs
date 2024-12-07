using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class SpecialShooterYellow : PlayerSpecialShooter
{
    const int WaveCount = 3;
    const float WaveSpacing = 5f;
    const int BulletCount = 24;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 5f;
    protected override float ShootingCooldown => 12f / 60;

    List<SpecialBullet> bullets = new(WaveCount * BulletCount);

    protected override IEnumerator Shoot()
    {
        bullets.Clear();
        yield return base.Shoot();

        Vector3 pos = transform.position;

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BulletSpacing);
                float s = Random.Range(0.2f, 1f) * BulletBaseSpeed;

                var bullet = SpawnProjectile(1, z, pos, false) as SpecialBullet;
                bullet.StartCoroutine(bullet.LerpSpeed(s, 0.1f, 1f));
                bullets.Add(bullet);
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        bullets.Randomize();
        yield return WaitForSeconds(0.5f);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                int b = (i * BulletCount) + ii;

                if (bullets[b].isActiveAndEnabled)
                {
                    bullets[b].Fire();
                }
            }
        }

        yield return WaitForSeconds(SpecialCooldown);
        canShoot = true;
    }
}