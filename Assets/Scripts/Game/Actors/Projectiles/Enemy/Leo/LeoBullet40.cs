using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LeoBullet40 : ScriptableBossBullet<LeoBossShooter4, BossBullet>
{
    [SerializeField] ProjectileObject bulletData;

    const int WaveCount = 2;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletRotationSpeed = 90;
    const float BulletRotationDuration = 9f;
    const float ShootingCooldown = 3f;

    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1.5f);
        StartCoroutine(FireBullets());
    }

    IEnumerator FireBullets()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = ii * BranchSpacing;
                Vector3 pos = transform.position;

                bulletData.colour = bulletData.gradient.Evaluate(i);

                var bullet = SpawnBullet(1, z, pos, false);
                bullet.StartCoroutine(bullet.RotateBy((i % 2 * 2 - 1) * BulletRotationSpeed, BulletRotationDuration));
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }

    protected override void Update()
    {
        base.Update();

        float t = currentLifetime / MaxLifetime;
        SpriteRenderer.color = projectileData.gradient.Evaluate(t);

        transform.eulerAngles = 180f * Vector3.forward;
    }
}