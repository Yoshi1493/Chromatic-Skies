using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LeoBullet40 : ScriptableEnemyBullet<LeoBulletSystem4, EnemyBullet>
{
    LeoBulletSystem4 bulletSystem;
    [SerializeField] ProjectileObject bulletData;

    const int RingCount = 5;
    const float RingSpacing = 5f;
    const int BulletCount = 18;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 1.5f;
    const float BulletSpeedModifier = 0.1f;
    const float BulletRotationSpeed = 120f;
    const float BulletRotationDuration = 1f;

    List<EnemyBullet> bullets = new(RingCount * BulletCount);

    protected override IEnumerator Move()
    {
        bullets.Clear();

        yield return WaitForSeconds(1.5f);

        for (int i = 0; i < RingCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (i * RingSpacing) + (ii * BulletSpacing);
                float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                Vector3 pos = transform.position;

                bulletData.colour = bulletData.gradient.Evaluate(i / (RingCount - 1f));

                var bullet = SpawnBullet(1, z, pos, false);
                bullet.StartCoroutine(bullet.LerpSpeed(0f, s, 1f));
                bullet.StartCoroutine(bullet.RotateBy(BulletRotationSpeed, 1f));
                bullet.StartCoroutine(bullet.RotateAround(transform.position, BulletRotationDuration, BulletRotationSpeed, delay: 1f));

                bullets.Add(bullet);
            }
        }

        yield return WaitForSeconds(1f);

        FireBullets();
    }

    void FireBullets()
    {
        for (int i = 0; i < RingCount; i++)
        {
            for (int ii = 0; ii < BulletCount; ii++)
            {
                int b = (i * BulletCount) + ii;
                var bullet = bullets[b];
                float s = BulletBaseSpeed + (i * BulletSpeedModifier * 2f);

                bullet.StartCoroutine(bullet.LerpSpeed(bullet.MoveSpeed, s, 1.5f));
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