using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem22 : EnemyShooter<Laser>
{
    const int LaserCount = 7;
    protected override float ShootingCooldown => 1.2f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        for (int i = 0; i < LaserCount; i++)
        {
            float z = transform.position.GetRotationDifference(PlayerPosition);
            Vector3 pos = Vector3.zero;

            bulletData.colour = bulletData.gradient.Evaluate(i / (LaserCount - 1f));
            SpawnProjectile(0, z, pos).Fire();

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}