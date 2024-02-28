using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LeoBullet40 : ScriptableEnemyBullet<LeoBulletSystem4, EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    const int WaveCount = 5;
    const float WaveSpacing = 360f / WaveCount;
    const int BranchCount = 12;
    const float BranchSpacing = 5f;
    const int BulletCount = 2;
    const float BulletSpawnRadius = 1.5f;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.05f;
    const float ShootingCooldown = 1f / 60;

    List<EnemyBullet> bullets = new(WaveCount * BranchCount * BulletCount);

    protected override IEnumerator Move()
    {
        bullets.Clear();

        yield return WaitForSeconds(1.5f);

        Vector3 v0 = transform.position;
        float r = Random.Range(0, WaveSpacing);

        for (int i = 0; i < WaveCount; i++)
        {
            Vector3 v1 = Vector3.up.RotateVectorBy((2 * i * WaveSpacing) + r);
            Vector3 v2 = Vector3.up.RotateVectorBy((2 * (i + 1) * WaveSpacing) + r);

            for (int ii = 0; ii < BranchCount; ii++)
            {
                Vector3 pos = BulletSpawnRadius * Vector3.Lerp(v1, v2, (float)ii / BranchCount) + v0;

                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (iii % 2 * 2 - 1) * ((i * WaveSpacing) + (ii * BranchSpacing));

                    bulletData.gradient.Evaluate(ii / (BranchCount - 1));

                    bullets.Add(SpawnBullet(1, z, pos, false));
                }

                yield return WaitForSeconds(ShootingCooldown);
            }
        }

        yield return WaitForSeconds(0.5f);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    int b = (i * BranchCount * BulletCount) + (ii * BulletCount) + iii;
                    float s = BulletBaseSpeed + (ii * BulletSpeedModifier);

                    bullets[b].StartCoroutine(bullets[b].LerpSpeed(0f, s, 2f));
                }
            }
        }
    }

    protected override void Update()
    {
        base.Update();

        float t = currentLifetime / MaxLifetime;
        spriteRenderer.color = projectileData.gradient.Evaluate(t);
    }
}