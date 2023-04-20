using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem41 : EnemyShooter<EnemyBullet>
{
    protected override float ShootingCooldown => 1f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2f);

        while (enabled)
        {
            float x = Random.Range(-screenHalfWidth, screenHalfWidth);
            float y = screenHalfHeight + 1f;
            Vector3 pos = new(x, y);
            float z = PlayerPosition.GetRotationDifference(pos);

            SpawnProjectile(8, z, pos, false).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}