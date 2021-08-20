using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem2 : EnemyBulletSystem
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                float z = j * 18;
                SpawnBullet(0, z, (i + 1) * transform.up.RotateVectorBy(z));
                z *= -1;
                z -= 18;
                SpawnBullet(0, z, (i + 1) * transform.up.RotateVectorBy(z));

                yield return WaitForSeconds(ShootingCooldown / 4);
            }

            yield return WaitForSeconds(ShootingCooldown);
        }



        yield return ownerShip.MoveToRandomPosition(1, 1);

        OnEnable();
    }
}