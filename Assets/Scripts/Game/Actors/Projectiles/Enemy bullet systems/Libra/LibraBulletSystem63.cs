using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem63 : EnemyShooter<EnemyBullet>
{
    public const int ParentBulletCount = 8;
    public const float ParentBulletSpacing = 360f / ParentBulletCount;
    const int RepeatCount = 2;
    const int WaveCount = 16;
    const int BulletCount = 40;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 5f;
    const float BulletSpeedModifier = -0.1f;
    const float BulletRotationSpeed = 90f;

    [HideInInspector] public List<EnemyBullet> bullets = new(ParentBulletCount);

    protected override IEnumerator Shoot()
    {
        enabled = false;
        yield break;

        for (int i = 0; i < ParentBulletCount; i++)
        {
            float z = (i + 0.5f) * ParentBulletSpacing;
            Vector3 pos = Vector3.zero;

            var bullet = SpawnProjectile(5, z, pos);
            bullet.Fire();
            bullets.Add(bullet);
        }

        yield return WaitForSeconds(1f);

        for (int i = 0; i < RepeatCount; i++)
        {
            SetSubsystemEnabled(1);
            yield return WaitForSeconds(4f);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    int d = iii % 2 * 2 - 1;
                    float z = iii * BulletSpacing;
                    float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(ii / (WaveCount - 1f));

                    var bullet = SpawnProjectile(6, z, pos);
                    bullet.StartCoroutine(bullet.RotateBy(d * BulletRotationSpeed, 2f, delay: 1f));
                    bullet.StartCoroutine(bullet.LerpSpeed(0f, s, 1f, delay: 1f));
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);
        }

        enabled = false;
    }
}