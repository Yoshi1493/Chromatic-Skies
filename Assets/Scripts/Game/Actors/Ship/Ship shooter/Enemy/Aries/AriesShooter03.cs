using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesShooter03 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 66;
    const int BulletCount = 6;
    const float ArcHalfWidth = 90f;
    const float SpawnRadiusModifier = 0.04f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                float r = PlayerPosition.GetRotationDifference(transform.position);

                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float z = Random.Range(-ArcHalfWidth, ArcHalfWidth) + r;
                    Vector3 pos = i * SpawnRadiusModifier * transform.up.RotateVectorBy(z);

                    bulletData.colour = bulletData.gradient.Evaluate(Mathf.InverseLerp(-ArcHalfWidth, ArcHalfWidth, z - r));

                    SpawnProjectile(3, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(4f);
        }
    }
}