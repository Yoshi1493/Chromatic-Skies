using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class LibraBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 3;
    const float BulletSpeedMultiplier = 2f;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        SetSubsystemEnabled(2);

        yield return WaitForSeconds(2f);
        StartMoveAction?.Invoke();

        while (enabled)
        {
            float r = RandomAngleDeg;
            float t = PlayerPosition.GetRotationDifference(transform.position);

            for (int i = 0; i < BulletCount; i++)
            {
                float z = t;
                float s = i + BulletSpeedMultiplier;
                Vector3 pos = transform.up.RotateVectorBy(r);

                var bullet = SpawnProjectile(0, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}