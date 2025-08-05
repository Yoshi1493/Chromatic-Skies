using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesShooter01 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 66;
    const int BulletCount = 4;
    const float BulletSpacing = 360f / BulletCount;
    const float SpawnRadiusModifier = 0.02f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.2f);

        for (int i = 0; i < WaveCount; i++)
        {
            float r = Random.Range(0f, BulletSpacing);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (ii * BulletSpacing) + r;
                Vector3 pos = i * SpawnRadiusModifier * transform.up.RotateVectorBy(z);

                SpawnProjectile(1, z, pos).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}