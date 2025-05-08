using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusShooter01 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 3;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2.5f);

        float z = PlayerPosition.GetRotationDifference(transform.position);
        Vector3 pos = Vector3.zero;

        for (int i = 0; i < BulletCount; i++)
        {
            SpawnProjectile(0, z, pos).Fire();
            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}