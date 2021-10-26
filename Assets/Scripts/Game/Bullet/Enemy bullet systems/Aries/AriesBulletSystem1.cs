using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem1 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        yield return WaitForSeconds(1f);

        while (enabled)
        {
            float z = 0f;

            for (int i = 0; i < 45; i++)
            {
                z += i;

                SpawnProjectile(0, z, transform.up.RotateVectorBy(z)).Fire();
                SpawnProjectile(0, z + 180f, transform.up.RotateVectorBy(z + 180f)).Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

            for (int i = 44; i > 0; i--)
            {
                z += i;

                SpawnProjectile(0, z, transform.up.RotateVectorBy(z)).Fire();
                SpawnProjectile(0, z + 180f, transform.up.RotateVectorBy(z + 180f)).Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(1f, 2f);
        }
    }
}