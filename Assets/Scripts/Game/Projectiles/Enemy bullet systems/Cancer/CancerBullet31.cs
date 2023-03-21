using System.Collections;
using UnityEngine;

public class CancerBullet31 : ScriptableEnemyBullet<CancerBulletSystem3, EnemyBullet>
{
    const int BulletCount = 12;
    const float BulletSpacing = 360f / BulletCount;

    protected override float MaxLifetime => 0.5f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0f, MaxLifetime);
    }

    public override void Destroy()
    {
        MoveSpeed = 0f;

        for (int i = 0; i < BulletCount; i++)
        {
            float z = i * BulletSpacing;
            Vector3 pos = transform.position;

            SpawnBullet(2, z, pos, false).Fire();
        }

        base.Destroy();
    }
}