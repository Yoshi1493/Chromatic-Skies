using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesShooter03 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 66;
    const int BulletCount = 6;
    const float BulletSpacing = 360f / BulletCount;
    const float SpawnRadiusModifier = 0.02f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                float r = Random.Range(0f, BulletSpacing);

                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = (ii * BulletSpacing) + r;
                    Vector3 pos = i * SpawnRadiusModifier * transform.up.RotateVectorBy(z);

                    SpawnProjectile(3, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(4f);
        }
    }
}