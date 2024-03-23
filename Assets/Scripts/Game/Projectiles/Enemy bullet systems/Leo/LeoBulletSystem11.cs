using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem11 : EnemyShooter<EnemyBullet>
{
    const int RepeatCount = 3;
    const int WaveCount = 3;
    const float WaveSpacing = BranchSpacing / WaveCount;
    const int BranchCount = 12;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletBaseSpeed = 3f;
    const float BulletSpeedModifier = 1f;
    const float BulletRotationSpeed = 30f;
    const float BulletRotationSpeedModifier = 15f;
    const float BulletRotationDuration = 3f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < RepeatCount; i++)
        {
            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    int d = i % 2 * 2 - 1;
                    float z = d * ((ii * WaveSpacing) + (iii * BranchSpacing));
                    float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                    float r = d * (BulletRotationSpeed + (i * BulletRotationSpeedModifier));
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(i % 2);

                    var bullet = SpawnProjectile(1, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(0.5f, s, 2f, delay: 1f));
                    bullet.StartCoroutine(bullet.RotateBy(r, BulletRotationDuration));
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);
        }

        enabled = false;
    }
}