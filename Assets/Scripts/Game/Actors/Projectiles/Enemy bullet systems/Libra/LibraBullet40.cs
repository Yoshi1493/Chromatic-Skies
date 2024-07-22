using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LibraBullet40 : ScriptableEnemyBullet<LibraBulletSystem4, EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    const int WaveCount = 40;
    const float WaveSpacing = 0.32f;
    const float BulletSpacing = 6f;
    const float ShootingCooldown = 1 / 60f;

    List<EnemyBullet> bullets = new(WaveCount);

    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0f, 1f);
        yield return this.RotateBy(180f, 0f);
        yield return this.LerpSpeed(0f, 10f, 2f);
        yield return WaitUntil(() => transform.position.y < (-5f - spriteRenderer.size.y));
        MoveSpeed = 0f;

        for (int i = 0; i < WaveCount; i++)
        {
            int r = i % 2;
            float z = transform.eulerAngles.z + ((r * 2 - 1) * ((i * BulletSpacing) + 15f));
            Vector3 pos = transform.position + (i * WaveSpacing * -moveDirection);

            bulletData.colour = bulletData.gradient.Evaluate(r);

            var bullet = SpawnBullet(1, z, pos, false) as LibraBullet41;
            bullet.DestroyAction += OnSpawnedBulletDestroy;
            bullets.Add(bullet);

            yield return WaitForSeconds(ShootingCooldown);
        }
    }

    public override void Destroy()
    {
        bullets.ForEach(b => b.Fire());
        bullets.Clear();

        base.Destroy();
    }

    void OnSpawnedBulletDestroy(EnemyBullet bullet)
    {
        bullets.Remove(bullet);
    }
}