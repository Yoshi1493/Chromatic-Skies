using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CancerBossShooter1 : BossShooter<BossBullet>
{
    const int WaveCount = 3;
    const float WaveSpacing = BulletSpacing / 2f;
    const int BulletCount = 15;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 5f;
    const float BulletSpeedModifier = -1f;

    List<BossBullet> bullets = new(BulletCount);

    protected override float ShootingCooldown => 0.4f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            bullets.Clear();

            float r = RandomAngleDeg;

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * -BulletSpacing) + r;
                    float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                    Vector3 pos = Vector3.zero;

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(s, 0f, 1f));
                    bullets.Add(bullet);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(0.5f);
            StartMoveAction?.Invoke();
            SetSubsystemEnabled(1);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    int b = (i * BulletCount) + ii;

                    if (bullets[b].isActiveAndEnabled)
                    {
                        bullets[b].Destroy();
                    }

                    yield return WaitForSeconds(0.05f);
                }
            }

            yield return WaitForSeconds(3f);
        }
    }
}