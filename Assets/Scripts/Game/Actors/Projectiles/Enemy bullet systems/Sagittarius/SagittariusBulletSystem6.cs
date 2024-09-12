using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem6 : EnemyShooter<EnemyBullet>
{
    GlobalLightController globalLight;
    AnimationCurve lightFadeInterpolation = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    EnemyPositionDisplay enemyPositionDisplay;

    const int WaveCount = 24;
    const float WaveSpacing = 7f;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 6;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletRotationSpeed = 90f;
    const float BulletRotationDuration = 4.5f;

    List<EnemyBullet> bullets = new(BulletCount);

    protected override float ShootingCooldown => 0.2f;

    protected override void Awake()
    {
        base.Awake();

        globalLight = FindObjectOfType<GlobalLightController>();
        enemyPositionDisplay = FindObjectOfType<EnemyPositionDisplay>();
    }

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        globalLight.FadeIntensity(0f, 2f, lightFadeInterpolation);
        enemyPositionDisplay.SetActive(false);

        bullets.Clear();

        for (int i = 1; enabled; i *= -1)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = ii * BulletSpacing;
                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(0, z, pos);
                bullet.StartCoroutine(bullet.RotateBy(i * BulletRotationSpeed, BulletRotationDuration));
                bullet.Fire();
                bullets.Add(bullet);
            }

            yield return WaitForSeconds(1f);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    for (int iv = 0; iv < BulletCount; iv++)
                    {
                        float z = (ii * WaveSpacing) + (iii * BranchSpacing);
                        Vector3 pos = bullets[iv].transform.position;

                        SpawnProjectile(1, z, pos, false).Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(6f);

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(2f);
        }
    }

    void OnDisable()
    {
        enemyPositionDisplay.SetActive(true);
    }
}