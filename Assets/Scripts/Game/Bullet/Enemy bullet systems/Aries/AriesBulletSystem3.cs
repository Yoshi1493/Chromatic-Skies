using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem3 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return ownerShip.MoveTo(3 * Vector3.up, 1f);

        yield return base.Shoot();

        SetSubsystemEnabled(1);

        yield return WaitForSeconds(1f);

        while (enabled)
        {
            float rand = Random.Range(-90f, 90f);

            for (int j = 0; j <= 6; j++)
            {
                float z = rand + (j * 30f) - 120f;
                SpawnProjectile(0, z, transform.up.RotateVectorBy(rand)).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown * 3f);
        }
    }
}