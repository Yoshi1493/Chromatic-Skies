using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem2 : EnemyBulletSystem
{

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return base.Shoot();

            SetSubsystemEnabled(1, true);

            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0; j < 30; j++)
            //    {
            //        float z = (j * 12f) + (i * 6f);

            //        SpawnBullet(3, z, Vector2.zero).Fire();
            //        SpawnBullet(4, z, Vector2.zero).Fire();
            //    }

            //    yield return WaitForSeconds(ShootingCooldown * 8f);
            //}

            yield return WaitForSeconds(5f);

            yield return ownerShip.MoveToRandomPosition(1f, 5f);
        }
    }
}