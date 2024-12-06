using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class SpecialShooterYellow : PlayerSpecialShooter
{
    const int WaveCount = 3;
    const float WaveSpacing = 5f;
    const int BranchCount = 40;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletBaseSpeed = 5f;
    protected override float ShootingCooldown => 12f / 60;

    List<SpecialBullet> bullets = new(WaveCount * BranchCount);

    protected override IEnumerator Shoot()
    {
        bullets.Clear();

        yield return base.Shoot();

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BranchSpacing);
                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(1, z, pos) as SpecialBullet;
                bullet.StartCoroutine(bullet.LerpSpeed(BulletBaseSpeed, 0f, 1f));
                bullets.Add(bullet);
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        yield return WaitForSeconds(1f);

        bullets.ForEach(b => b.Fire());

        yield return WaitForSeconds(SpecialCooldown);
        canShoot = true;
    }
}