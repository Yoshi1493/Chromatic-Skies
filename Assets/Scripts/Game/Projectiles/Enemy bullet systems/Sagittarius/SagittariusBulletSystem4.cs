using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem4 : EnemyShooter<EnemyBullet>
{
    FlashlightEffect flashlightEffect;

    const int WaveCount = 44;
    const float WaveSpacing = 18f;
    const int BranchCount = 2;
    const int BulletCount = 5;
    const float BulletSpacing = 30f;
    const float BulletSpawnRadius = 1.5f;

    protected override void Awake()
    {
        base.Awake();
        flashlightEffect = FindObjectOfType<FlashlightEffect>();
    }

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        //flashlightEffect.enabled = true;
        //flashlightEffect.SetStengthOverTime(4f, 8f);
        //flashlightEffect.SetRadiusOverTime(0.4f, 8f);
        //flashlightEffect.SetHardnessOverTime(1.01f, 8f);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                float r = PlayerPosition.GetRotationDifference(transform.position);

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float t = (ii % 2 * 2 - 1) * ((i + 0.5f) * WaveSpacing);
                    bulletData.colour = bulletData.gradient.Evaluate(ii);

                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = (i * 0.5f * WaveSpacing) + ((iii - ((BulletCount - 1) / 2)) * BulletSpacing);
                        Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(t);

                        SpawnProjectile(0, z, pos).Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(5f);
        }

    }
}