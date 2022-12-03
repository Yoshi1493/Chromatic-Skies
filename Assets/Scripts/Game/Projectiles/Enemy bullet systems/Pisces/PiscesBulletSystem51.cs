using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem51 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 5;

    protected override float ShootingCooldown => 1.5f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float z = PlayerPosition.GetRotationDifference(transform.position);
                Vector3 pos = Vector3.zero;

                bulletData.colour = bulletData.gradient.Evaluate(i / (BulletCount - 1f));
                var bullet = SpawnProjectile(2, z, pos);
                bullet.MoveSpeed = (i * 0.5f) + 2f;
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}