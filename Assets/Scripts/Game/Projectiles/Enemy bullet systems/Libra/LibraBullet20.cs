using System.Collections;
using UnityEngine;
using static MathHelper;

public class LibraBullet20 : ScriptableEnemyBullet<LibraBulletSystem2, EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    const int BulletCount = 3;
    const float BulletSpacing = 360f / BulletCount;

    protected override float MaxLifetime => 0.6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0f, MaxLifetime);
    }

    public override void Destroy()
    {
        float r = RandomAngleDeg;

        for (int i = 0; i < BulletCount; i++)
        {
            float z = i * BulletSpacing + r;
            Vector3 pos = transform.position;

            bulletData.colour = bulletData.gradient.Evaluate(i / (BulletCount - 1f));
            SpawnBullet(1, z, pos, false).Fire();
        }

        base.Destroy();
    }
}