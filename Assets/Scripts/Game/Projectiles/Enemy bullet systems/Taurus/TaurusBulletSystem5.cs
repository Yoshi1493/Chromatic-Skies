using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 5;
    public const float WaveSpacing = -144f;

    List<EnemyBullet> bullets = new(WaveCount);

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(2);

        while (enabled)
        {
            StartMoveAction?.Invoke();

            bullets.Clear();

            for (int i = 0; i < WaveCount; i++)
            {
                yield return WaitForSeconds(ShootingCooldown);

                float z = (i + 1) * WaveSpacing;
                Vector3 pos = Vector3.zero;

                bullets.Add(SpawnProjectile(0, z, pos));
            }

            bullets.ForEach(b => b.Fire());

            yield return WaitForSeconds(1f);
            SetSubsystemEnabled(1);

            yield return WaitForSeconds(12f);
        }
    }
}