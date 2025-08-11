using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornShooter02 : CommonEnemyShooter<EnemyBullet>
{
    const int BulletCount = 18;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletRotationSpeed = 90f;
    const float BulletRotationDuration = 3f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(0.5f);

        float r = PlayerPosition.GetRotationDifference(transform.position);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = (i * BulletSpacing) + r;
            Vector3 pos = Vector3.zero;

            bulletData.colour = bulletData.gradient.Evaluate(i);

            var bullet = SpawnProjectile(2, z, pos);
            bullet.StartCoroutine(bullet.RotateBy((i % 2 * 2 - 1) * BulletRotationSpeed, BulletRotationDuration, delay: 1f));
            bullet.Fire();
        }
    }
}