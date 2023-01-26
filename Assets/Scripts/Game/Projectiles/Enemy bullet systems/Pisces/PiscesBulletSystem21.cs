using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem21 : EnemyShooter<EnemyBullet>
{
    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        float y = screenHalfHeight + 1f;
        float z = 0f;

        while (enabled)
        {
            float x = Random.Range(-screenHalfWidth, screenHalfWidth);
            Vector3 pos = new(x, y);

            SpawnProjectile(1, z, pos, false).Fire();
            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}