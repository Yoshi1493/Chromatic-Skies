using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 36;
    const int BulletSpacing = 360 / BulletCount;
    const float BulletOffset = BulletSpacing / 8f;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedMultiplier = 0.1f;
    const float BulletSpeedMultiplierMultiplier = 0.8f;

    protected override float ShootingCooldown => 0.04f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float z = i * BulletSpacing + BulletOffset;
                float s = BulletBaseSpeed + (i * BulletSpeedMultiplier);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i / (BulletCount - 1f));
                var bullet = SpawnProjectile(0, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();

                bullet = SpawnProjectile(0, z + (0.5f * BulletSpacing), pos);
                bullet.MoveSpeed = s * BulletSpeedMultiplierMultiplier;
                bullet.Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(ShootingCooldown * 2f);

            for (int i = 0; i < BulletCount; i++)
            {
                float z = (BulletCount - i) * BulletSpacing - BulletOffset;
                float s = BulletBaseSpeed + (i * BulletSpeedMultiplier);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i / (BulletCount - 1f));
                var bullet = SpawnProjectile(0, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();

                bullet = SpawnProjectile(0, z - (0.5f * BulletSpacing), pos);
                bullet.MoveSpeed = s * BulletSpeedMultiplierMultiplier;
                bullet.Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

            SetSubsystemEnabled(1);
            yield return WaitForSeconds(1.5f);

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(1f);
        }
    }
}