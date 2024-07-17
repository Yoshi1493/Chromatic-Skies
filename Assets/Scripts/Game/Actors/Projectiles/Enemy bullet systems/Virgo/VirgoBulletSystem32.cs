using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem32 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 72;
    readonly float BulletSpacing = (1f + Mathf.Sqrt(5f)) * 180f;
    const float BulletRotationSpeed = 45f;
    const float BulletRotationDuration = 2f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < BulletCount; i++)
        {
            //print($"[{i}]: {Time.timeSinceLevelLoad}");
            int d = i % 2 * 2 - 1;
            float z = (i * BulletSpacing) % 360f;
            Vector3 pos = Vector3.zero;

            var bullet = SpawnProjectile(2, z, pos);
            bullet.StartCoroutine(bullet.RotateBy(d * BulletRotationSpeed, BulletRotationDuration));
            bullet.Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}