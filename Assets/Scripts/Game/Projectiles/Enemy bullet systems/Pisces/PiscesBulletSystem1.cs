using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem1 : EnemyShooter<EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    const int BulletCount = 36;
    const int BulletSpacing = 360 / BulletCount;
    const float BulletOffset = BulletSpacing * 0.125f;

    protected override float ShootingCooldown => 0.04f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float z = i * BulletSpacing + BulletOffset;
                float spd = 2 + (i / 10f);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i / (BulletCount - 1f));
                var bullet = SpawnProjectile(0, z, pos);
                bullet.MoveSpeed = spd;
                bullet.Fire();

                bullet = SpawnProjectile(0, z + (0.5f * BulletSpacing), pos);
                bullet.MoveSpeed = spd * 0.8f;
                bullet.Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(ShootingCooldown * 2f);

            for (int i = 0; i < BulletCount; i++)
            {
                float z = (BulletCount - i) * BulletSpacing - BulletOffset;
                float spd = 2 + (i / 10f);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i / (BulletCount - 1f));
                var bullet = SpawnProjectile(0, z, pos);
                bullet.MoveSpeed = spd;
                bullet.Fire();

                bullet = SpawnProjectile(0, z - (0.5f * BulletSpacing), pos);
                bullet.MoveSpeed = spd * 0.8f;
                bullet.Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            SetSubsystemEnabled(1);
            yield return ownerShip.MoveToRandomPosition(1f, delay: 1f);
        }
    }
}