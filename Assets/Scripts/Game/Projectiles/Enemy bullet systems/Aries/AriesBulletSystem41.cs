using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem41 : EnemyShooter<EnemyBullet>
{
    const int AngleLimit = 90;

    protected override IEnumerator Shoot()
    {
        int i = Random.value > 0.5f ? 0 : AngleLimit;

        float y = screenHalfHeight + 1f;

        while (enabled)
        {
            float x = Random.Range(-screenHalfWidth, screenHalfWidth);
            float z = Mathf.PingPong(i, AngleLimit) - (AngleLimit * 0.5f);
            Vector3 pos = new(x, y);

            SpawnProjectile(1, z, pos, false).Fire();
            yield return WaitForSeconds(ShootingCooldown);

            i++;
        }
    }
}