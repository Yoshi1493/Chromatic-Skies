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

            for (int ii = 0; ii < BigBulletCount; ii++)
            {
                Vector3 pos = bullets.Dequeue().transform.position;

                for (int iii = 0; iii < SmallBulletCount; iii++)
                {
                    float z = (ii * BigBulletSpacing) + (iii * SmallBulletSpacing);

                    SpawnProjectile(1, z, pos, false).Fire();
                }
            }
        }

    }
}