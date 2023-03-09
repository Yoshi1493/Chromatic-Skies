using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 60;
    const int BulletCount = 3;
    const int BulletSpacing = 360 / BulletCount;

    protected override float ShootingCooldown => 0.15f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            yield return WaitForSeconds(3f);

            for (int i = 0; i < WaveCount; i++)
            {
                float r = Random.Range(0, BulletSpacing);

                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = (ii * BulletSpacing) + r;
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(0, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            StartMoveAction?.Invoke();
        }
    }
}