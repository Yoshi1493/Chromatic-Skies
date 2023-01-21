using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 11;
    const float WaveSpacing = 105f;
    const int BulletCount = 10;
    const float BulletSpacing = 10f;
    const float MinRadius = 1f;
    const float MaxRadius = 2f;

    protected override float ShootingCooldown => 0.04f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = (i * WaveSpacing) + ((ii - ((BulletCount - 1) / 2f)) * BulletSpacing);

                    Vector3 v1 = Vector3.up.RotateVectorBy(i * WaveSpacing);
                    Vector3 v2 = Vector3.up.RotateVectorBy((i + 1) * WaveSpacing);
                    Vector3 pos = MinRadius * Vector3.Lerp(v1, v2, (float)ii / BulletCount);

                    SpawnProjectile(0, z, pos).Fire();

                    pos = MaxRadius * pos.RotateVectorBy(180f);
                    SpawnProjectile(0, z, pos).Fire();

                    yield return WaitForSeconds(ShootingCooldown);
                }
            }

            yield return WaitForSeconds(2f);

            AttackFinishAction?.Invoke();
            yield return WaitForSeconds(2f);
        }
    }
}