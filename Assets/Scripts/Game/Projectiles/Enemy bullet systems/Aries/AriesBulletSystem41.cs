using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem41 : EnemyShooter<EnemyBullet>
{
    const int AngleLimit = 90;

    protected override IEnumerator Shoot()
    {
        int n = Random.value > 0.5f ? 0 : AngleLimit;

        float y = screenHalfHeight + 1f;

        while (enabled)
        {
            float z = Mathf.PingPong(n, AngleLimit) - (AngleLimit * 0.5f);
            float x = Random.Range(-screenHalfWidth, screenHalfWidth);
            Vector3 pos = new(x, y);

            SpawnProjectile(1, z, pos, false).Fire();
            yield return WaitForSeconds(ShootingCooldown);

            n++;
        }
    }
}