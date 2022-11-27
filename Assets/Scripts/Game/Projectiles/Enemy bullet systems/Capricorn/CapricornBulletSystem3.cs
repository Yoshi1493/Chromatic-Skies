using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem3 : EnemyShooter<EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    const int WaveCount = 11;
    const float WaveSpacing = 360f / (2 * (WaveCount - 1));
    const int BulletCount = 8;
    const float BulletSpeed = 1.5f;

    protected override float ShootingCooldown => 0.25f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        yield return WaitForSeconds(3f);

        while (enabled)
        {
            float r = PlayerPosition.GetRotationDifference(transform.position);

            for (int i = 0; i < WaveCount; i++)
            {
                int BranchCount = i % (WaveCount - 1) == 0 ? 1 : 2;

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (i * WaveSpacing) * (-ii * 2 + 1) + r;
                    Vector3 pos = Vector3.zero;

                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float s = BulletSpeed + (iii * 0.5f);
                        bulletData.colour = bulletData.gradient.Evaluate((float)iii / BulletCount);

                        var bullet = SpawnProjectile(0, z, pos);
                        bullet.MoveSpeed = s;
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(2f, delay: 2f);
        }
    }
}