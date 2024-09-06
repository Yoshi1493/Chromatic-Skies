using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem31 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const int BulletCount = 12;
    const float BulletBaseSpeed = 1f;
    const float BulletRotationSpeed = 90f;
    const float BulletRotationSpeedModifier = -15f;
    const float BulletRotationDuration = 2f;
    const float BulletRotationDurationModifier = 0.1f;

    List<EnemyBullet> bullets = new(WaveCount * BulletCount);

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        bullets.Clear();

        for (int i = 0; i < WaveCount; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);

            Vector3 v = PlayerPosition - transform.position;

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = v.GetRotationDifference(Vector3.zero);
                float s = Mathf.Lerp(BulletBaseSpeed, v.magnitude, ii / (BulletCount - 1f));
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                var bullet = SpawnProjectile(1, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();
                bullets.Add(bullet);
            }
        }

        yield return WaitForSeconds(2f);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                int b = i * BulletCount + ii;
                float r = (ii % 2 * 2 - 1) * (BulletRotationSpeed + (ii / 2 * BulletRotationSpeedModifier));
                float d = BulletRotationDuration + (ii / 2 * BulletRotationDurationModifier);

                bullets[b].StartCoroutine(bullets[b].RotateBy(r, d));
                bullets[b].StartCoroutine(bullets[b].LerpSpeed(0f, 2f, 1f));

                yield return WaitForSeconds(ShootingCooldown * 0.1f);
            }
        }

        enabled = false;
    }
}