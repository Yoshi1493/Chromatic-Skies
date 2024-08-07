using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem63 : EnemyShooter<EnemyBullet>
{
    public const int ParentBulletCount = 8;
    public const float ParentBulletSpacing = 360f / ParentBulletCount;
    const int WaveCount = 80;
    const int BulletCount = 3;
    const float BulletSpacing = 15f;
    const float BulletBaseSpeed = 8f;
    const float BulletSpeedModifier = -0.05f;

    List<EnemyBullet> bullets = new(ParentBulletCount);

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        //enabled = false;
        //yield break;

        for (int i = 0; i < ParentBulletCount; i++)
        {
            float z = (i + 0.5f) * ParentBulletSpacing;
            Vector3 pos = Vector3.zero;

            var bullet = SpawnProjectile(5, z, pos);
            bullet.Fire();
            bullets.Add(bullet);
        }

        yield return WaitForSeconds(1f);
        SetSubsystemEnabled(1);

        yield return WaitForSeconds(3f);

        for (int i = 0; i < WaveCount; i++)
        {
            float r = PlayerPosition.GetRotationDifference(transform.position) + Random.Range(-1f, 1f);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float t = (ii - ((BulletCount - 1) / 2f)) * BulletSpacing;
                float z = t + r;
                float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(6, z, pos);
                bullet.StartCoroutine(bullet.LerpSpeed(s, 0f, 1f));
                bullet.StartCoroutine(bullet.RotateBy(t * 2f, 0f, delay: 2f));
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}