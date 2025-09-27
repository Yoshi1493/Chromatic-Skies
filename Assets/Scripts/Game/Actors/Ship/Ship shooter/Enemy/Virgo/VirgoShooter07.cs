using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class VirgoShooter07 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 36;
    const int BulletMinCount = 3;
    const int BulletMaxCount = 7;
    const float BulletSpawnRadius = 1f;
    const float SpawnRadiusModifier = -0.05f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        for (int ii = 0; ii < WaveCount; ii++)
        {
            int bulletCount = Random.Range(BulletMinCount, BulletMaxCount);

            for (int iv = 0; iv < bulletCount; iv++)
            {
                float z = RandomAngleDeg;
                Vector3 pos = (BulletSpawnRadius + (ii * SpawnRadiusModifier)) * transform.up.RotateVectorBy(z);

                SpawnProjectile(7, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}