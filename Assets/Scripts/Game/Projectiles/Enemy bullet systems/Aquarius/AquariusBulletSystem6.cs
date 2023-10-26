using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class AquariusBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 6;
    const float BulletMinSpeed = 1f;
    const float BulletMaxSpeed = 5f;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        StartMoveAction?.Invoke();
        SetSubsystemEnabled(1);

        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float z = RandomAngleDeg;
                float s = Random.Range(BulletMinSpeed, BulletMaxSpeed);
                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(0, z, pos);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}