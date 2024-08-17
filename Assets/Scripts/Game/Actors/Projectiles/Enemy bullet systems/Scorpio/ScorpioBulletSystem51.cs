using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem51 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 20;
    const float WaveSpacing = 3f;
    const int BranchCount = 18;
    const float BranchSpacing = 360f / BranchCount;
    const float SpawnRadiusModifier = 0.25f;
    const float BulletBaseSpeed = -1f;
    const float BulletSpeedModifier = 0.1f;
    const float BulletRotationSpeed = 90f;

    protected override float ShootingCooldown => 0.05f;

    Stack<EnemyBullet> bullets = new(WaveCount * BranchCount);

    protected override IEnumerator Shoot()
    {
        for (int i = 1; enabled; i *= -1)
        {
            bullets.Clear();
            yield return WaitForSeconds(5f);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float z = i * ((ii * WaveSpacing) + (iii * BranchSpacing));
                    float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                    Vector3 pos = ii * SpawnRadiusModifier * transform.up.RotateVectorBy(z);

                    bulletData.colour = bulletData.gradient.colorKeys[0].color;

                    var bullet = SpawnProjectile(2, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(s, 0f, 1f));
                    bullets.Push(bullet);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    if (bullets.TryPop(out EnemyBullet bullet))
                    {
                        if (bullet.isActiveAndEnabled)
                        {
                            bullet.StartCoroutine(bullet.RotateBy(i * BulletRotationSpeed, 1f));
                            bullet.Fire();
                        }
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(7f);
        }
    }

}