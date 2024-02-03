using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int BigBulletCount = 30;
    const float BigBulletSpacing = 360f / BigBulletCount;
    const float BigBulletRotationSpeed = 45f;
    public const float BigBulletRotationDuration = 1f;
    const float SmallBulletCount = 3;
    const float SmallBulletSpacing = 360f / SmallBulletCount;

    Queue<EnemyBullet> bullets = new(BigBulletCount);

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        bullets.Clear();

        for (int i = 1; enabled; i *= -1)
        {
            StartCoroutine(BatchDequeue());

            for (int ii = 0; ii < BigBulletCount; ii++)
            {
                float z = ii * BigBulletSpacing;
                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(0, z, pos);
                bullet.StartCoroutine(bullet.RotateBy(i * BigBulletRotationSpeed, BigBulletRotationDuration));
                bullet.Fire();
                bullets.Enqueue(bullet);
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }

    IEnumerator BatchDequeue()
    {
        yield return WaitForSeconds(BigBulletRotationDuration);

        for (int i = 0; i < BigBulletCount; i++)
        {
            if (bullets.TryDequeue(out EnemyBullet bullet))
            {
                float r = Random.Range(0f, SmallBulletSpacing);

                for (int ii = 0; ii < SmallBulletCount; ii++)
                {
                    float z = ii * SmallBulletSpacing + r;
                    Vector3 pos = bullet.transform.position;

                    SpawnProjectile(1, z, pos, false).Fire();
                }
            }
        }

        yield break;
    }

}