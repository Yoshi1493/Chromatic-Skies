using System.Collections;
using UnityEngine;
using static MathHelper;

public class LibraBullet22 : ScriptableEnemyBullet<LibraBulletSystem2, EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    const int BulletCount = 12;
    const float BulletSpacing = 360f / BulletCount;

    protected override float MaxLifetime => 1f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0f, MaxLifetime);
    }

    public override void Destroy()
    {
        float r = RandomAngleDeg;

        for (int i = 0; i < BulletCount; i++)
        {
            float z = i * BulletSpacing + r;
            Vector3 pos = transform.position;

            bulletData.colour = spriteRenderer.color;
            SpawnBullet(3, z, pos, false).Fire();
        }

        base.Destroy();
    }
}