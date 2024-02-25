using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LeoBullet40 : ScriptableEnemyBullet<LeoBulletSystem4, EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    const int BranchCount = 5;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 12;
    const float BulletSpacing = 10f;
    const float BulletSpawnRadius = 1.5f;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.1f;
    const float ShootingCooldown = 1f / 60;

    List<EnemyBullet> bullets = new(BranchCount * BulletCount);

    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1.5f);

        Vector3 v0 = transform.position;
        float r = Random.Range(0, BranchSpacing);

        for (int i = 0; i < BranchCount; i++)
        {
            Vector3 v1 = Vector3.up.RotateVectorBy((2 * i * BranchSpacing) + r);
            Vector3 v2 = Vector3.up.RotateVectorBy((2 * (i + 1) * BranchSpacing) + r);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (i * BranchSpacing) + (ii * BulletSpacing);
                Vector3 pos = BulletSpawnRadius * Vector3.Lerp(v1, v2, (float)ii / BulletCount) + v0;

                bulletData.gradient.Evaluate(ii / (BulletCount - 1));

                bullets.Add(SpawnBullet(1, z, pos, false));
                yield return WaitForSeconds(ShootingCooldown);
            }
        }

        yield return WaitForSeconds(0.5f);

        for (int i = 0; i < BranchCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                int b = (i * BulletCount) + ii;
                float s = BulletBaseSpeed + (ii * BulletSpeedModifier);

                bullets[b].StartCoroutine(bullets[b].LerpSpeed(0f, s, 2f));
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