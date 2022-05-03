using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem11 : EnemyBulletSubsystem<EnemyBullet>
{
    const int WaveCount = 55;
    const float RotationPerWave = 10f;
    const int BulletCount = 5;
    const int BulletSpacing = 360 / BulletCount;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(1f);

            float randStartAngle = Random.Range(-BulletSpacing, BulletSpacing);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int j = 0; j < BulletCount; j++)
                {
                    float z = (i * RotationPerWave) + (j * BulletSpacing) + randStartAngle;
                    SpawnProjectile(1, z, transform.up.RotateVectorBy(z) / 2f).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(1f, delay: 2f);
        }
    }
}