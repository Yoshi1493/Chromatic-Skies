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

        flashlightEffect.enabled = true;
        flashlightEffect.SetStengthOverTime(5f, 12f);
        flashlightEffect.SetHardnessOverTime(1.1f, 10f);

        SetSubsystemEnabled(1);

        for (int i = 1; enabled; i *= -1)
        {
            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float t = (iii % 2 * 2 - 1) * ii * WaveSpacing;
                    bulletData.colour = bulletData.gradient.Evaluate(iii); 

                    for (int iv = 0; iv < BulletCount; iv++)
                    {
                        float z = i * ((ii * WaveSpacing) + (iv * BulletSpacing));
                        Vector3 pos = ii * SpawnRadiusModifier * transform.up.RotateVectorBy(t);

                        SpawnProjectile(0, z, pos).Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(2f);
        }
    }

    protected override void OnLoseLife()
    {
        base.OnLoseLife();
        flashlightEffect.enabled = false;
    }
}