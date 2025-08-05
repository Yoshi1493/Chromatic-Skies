using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesShooter08 : CommonEnemyShooter<EnemyBullet>
{
    const int BulletCount = 3;
    const float BulletSpacing = 15f;

    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(ShootingCooldown);

        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = ((i - ((BulletCount - 1) / 2f)) * BulletSpacing) + r;
            Vector3 pos = Vector3.zero;

            bulletData.colour = bulletData.gradient.Evaluate(i % 2);

            SpawnProjectile(8, z, pos).Fire();
        }
    }

}