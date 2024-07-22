using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem4 : EnemyShooter<EnemyBullet>
{
    FlashlightEffect flashlightEffect;

    const int WaveCount = 48;
    const float WaveSpacing = 16f;
    const int BranchCount = 2;
    const int BulletCount = 4;
    const float BulletSpacing = 360f / BulletCount;
    const float SpawnRadiusModifier = 0.04f;

    protected override void Awake()
    {
        base.Awake();
        flashlightEffect = FindObjectOfType<FlashlightEffect>();
    }

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        //flashlightEffect.enabled = true;
        //flashlightEffect.SetStengthOverTime(4f, 0f);
        //flashlightEffect.SetRadiusOverTime(0f, 0f);
        //flashlightEffect.SetHardnessOverTime(1f, 0f);

        //flashlightEffect.SetRadiusOverTime(0f, 10f);
        //flashlightEffect.SetHardnessOverTime(0.5f, 10f);

        SetSubsystemEnabled(1);

        while (enabled)
        {
            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float t = (ii % 2 * 2 - 1) * i * WaveSpacing;
                    bulletData.colour = bulletData.gradient.Evaluate(ii);

                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = (i * WaveSpacing) + (iii * BulletSpacing);
                        Vector3 pos = i * SpawnRadiusModifier * transform.up.RotateVectorBy(t);

                        SpawnProjectile(0, z, pos).Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(2f);
        }

    }
}