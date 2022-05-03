using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem31 : EnemyBulletSubsystem<Laser>
{
    const int LaserCount = 99;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(2f);

            for (int i = 0; i < LaserCount; i++)
            {
                float z = Random.Range(0f, 360f);
                SpawnProjectile(0, z, -transform.up.RotateVectorBy(z)).Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

            StartCoroutine(ownerShip.MoveToRandomPosition(1f));
        }
    }
}