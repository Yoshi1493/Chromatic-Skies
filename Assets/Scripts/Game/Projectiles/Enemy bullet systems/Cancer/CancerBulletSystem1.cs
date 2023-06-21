using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CancerBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const float WaveSpacing = BulletSpacing / 2f;
    const int BulletCount = 15;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 5f;

    Stack<EnemyBullet> bullets = new(BulletCount);

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            float r = RandomAngleDeg;

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * -BulletSpacing) + r;
                    Vector3 pos = Vector3.zero;

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(BulletBaseSpeed - i, 0f, 1f));
                    bullets.Push(bullet);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(0.5f);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    bullets.Pop().Destroy();
                    yield return WaitForSeconds(ShootingCooldown * 0.25f);
                }
            }

            yield return WaitForSeconds(0.5f);

            StartMoveAction?.Invoke();
            SetSubsystemEnabled(1);

            yield return WaitForSeconds(3f);
        }
    }
}