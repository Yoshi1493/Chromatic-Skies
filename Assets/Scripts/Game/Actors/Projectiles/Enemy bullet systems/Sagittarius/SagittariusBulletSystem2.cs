using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem2 : EnemyShooter<EnemyBullet>
{
    FlashlightEffect flashlightEffect;

    const int WaveCount = 240;
    const float WaveSpacing = 360f / WaveCount;
    const float BulletRotationSpeed = 90f;
    const float BulletRotationDuration = 5f;

    protected override float ShootingCooldown => 1f / 60;

    protected override void Awake()
    {
        base.Awake();
        flashlightEffect = FindObjectOfType<FlashlightEffect>();
    }

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        flashlightEffect.enabled = true;
        flashlightEffect.SetStengthOverTime(4f, 15f);

        StartMoveAction?.Invoke();
        SetSubsystemEnabled(1);

        for (int i = 1; enabled; i *= -1)
        {
            float t = 0f;

            for (int ii = 0; ii < WaveCount; ii++)
            {
                float z = i * (ii * WaveSpacing + t);
                Vector3 pos = transform.up.RotateVectorBy(i * ii * WaveSpacing);

                bulletData.colour = bulletData.gradient.Evaluate(ii % 2);

                var bullet = SpawnProjectile(0, z, pos);
                bullet.StartCoroutine(bullet.RotateBy((ii % 2 * 2 - 1) * BulletRotationSpeed, BulletRotationDuration, delay: 0.5f));
                bullet.Fire();

                yield return WaitForSeconds(ShootingCooldown);
                t = (t + 5f) % 360f;
            }

            yield return WaitForSeconds(2f);
        }
    }

    protected override void OnLoseLife()
    {
        base.OnLoseLife();
        flashlightEffect.enabled = false;
    }
}