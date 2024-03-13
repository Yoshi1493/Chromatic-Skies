using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class LeoBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int BulletMinCount = 3;
    const int BulletMaxCount = 7;
    const float BulletSpawnRadius = 1.25f;
    const float SpawnRadiusModifier = 0.1f;

    protected override float ShootingCooldown => 2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        //SetSubsystemEnabled(1);

        while (enabled)
        {
            int bulletCount = Random.Range(BulletMinCount, BulletMaxCount);
            List<Vector3> bulletPositions = GetRandomPointsAlongBounds(new Vector3(-screenHalfWidth, -screenHalfHeight), new Vector3(screenHalfWidth, screenHalfHeight), bulletCount);

            for (int i = 0; i < bulletPositions.Count; i++)
            {
                Vector3 pos = (BulletSpawnRadius + (i * SpawnRadiusModifier)) * bulletPositions[i];
                float z = transform.position.GetRotationDifference(pos);

                SpawnProjectile(0, z, pos, false).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}