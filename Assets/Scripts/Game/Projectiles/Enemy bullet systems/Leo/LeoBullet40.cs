using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LeoBullet40 : ScriptableEnemyBullet<LeoBulletSystem4, EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    const int RepeatCount = 2;
    const int WaveCount = 5;
    const float WaveSpacing = 360f / WaveCount;
    const int BranchCount = 12;
    const float BranchSpacing = 8f;
    const float BulletSpawnRadius = 1.5f;
    const float BulletBaseSpeed = 1.5f;
    const float BulletSpeedModifier = 0.05f;
    const float ShootingCooldown = 1f / 60;

    List<EnemyBullet> bullets = new(WaveCount * BranchCount);

    protected override IEnumerator Move()
    {
        for (int i = 0; i < RepeatCount; i++)
        {
            bullets.Clear();

            yield return WaitForSeconds(1.5f);

            Vector3 v0 = transform.position;
            float r = Random.Range(0, WaveSpacing);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                Vector3 v1 = Vector3.up.RotateVectorBy((2 * ii * WaveSpacing) + r);
                Vector3 v2 = Vector3.up.RotateVectorBy((2 * (ii + 1) * WaveSpacing) + r);

                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float z = (ii * WaveSpacing) + (iii * BranchSpacing) + r;
                    Vector3 pos = BulletSpawnRadius * Vector3.Lerp(v1, v2, (float)iii / BranchCount) + v0;

                    bulletData.gradient.Evaluate(iii / (BranchCount - 1));

                    bullets.Add(SpawnBullet(1, z, pos, false));

                    yield return WaitForSeconds(ShootingCooldown);
                }
            }

            yield return WaitForSeconds(0.5f);

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    int b = (ii * BranchCount) + iii;
                    float s = BulletBaseSpeed + (iii * BulletSpeedModifier);

                    bullets[b].StartCoroutine(bullets[b].LerpSpeed(0f, s, 2f));
                }
            }

            yield return WaitForSeconds(1.5f);
        }
    }

    protected override void Update()
    {
        base.Update();

        float t = currentLifetime / MaxLifetime;
        spriteRenderer.color = projectileData.gradient.Evaluate(t);
    }
}