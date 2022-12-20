using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem3 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            SetSubsystemEnabled(1);

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    float z = (j * 15f) + (i * 5f);
                    Vector3 pos = Vector3.zero;

                    SpawnProjectile(0, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown * 4f);
            }

            //yield return ownerShip.MoveToRandomPosition(1f, maxSqrMagDelta: 3, delay: 1f);
        }
    }
}