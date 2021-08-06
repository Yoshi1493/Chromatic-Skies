using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem2 : EnemyBulletSystem
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        float rotateAmount = 12f;

        while(enabled)
        {
            spawnPositions[0].RotateAround(transform.parent.position, Vector3.forward, rotateAmount);
            spawnPositions[1].RotateAround(transform.parent.position, Vector3.forward, rotateAmount);

            SpawnBullet(0, 0);
            SpawnBullet(0, 1);

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}