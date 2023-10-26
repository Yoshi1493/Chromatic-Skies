using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 1;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        StartMoveAction?.Invoke();
        SetSubsystemEnabled(1);

        while (enabled)
        {
            Vector3 v1 = PlayerPosition;

            for (int i = 0; i < BulletCount; i++)
            {
                float z = 0f;
                Vector3 pos;

                do
                {
                    pos.x = 0.8f * Random.Range(-screenHalfWidth, screenHalfWidth);
                    pos.y = 0.8f * Random.Range(-screenHalfHeight, screenHalfHeight);
                    pos.z = 0f;
                }
                while (pos.IsTooClose(v1, 2f));

                var bullet = SpawnProjectile(0, z, pos, false);
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}