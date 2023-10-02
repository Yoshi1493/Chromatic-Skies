using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBullet50 : ScriptableEnemyBullet<TaurusBulletSystem5, EnemyBullet>
{
    const int BulletCount = 5;
    const float BulletBaseSpeed = 3.2f;
    const float BulletSpeedMultiplier = 0.2f;
    const float ShootingCooldown = 0.2f;

    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        float z = playerShip.transform.position.GetRotationDifference(transform.position);

        for (int i = 0; i < BulletCount; i++)
        {
            float s = BulletBaseSpeed + (i * BulletSpeedMultiplier);
            Vector3 pos = transform.position;

            var bullet = SpawnBullet(5, z, pos, false);
            bullet.MoveSpeed = s;
            bullet.Fire();
        }

        yield return WaitForSeconds(ShootingCooldown);
    }
}