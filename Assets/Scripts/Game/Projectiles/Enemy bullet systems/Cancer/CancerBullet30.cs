using System.Collections;
using UnityEngine;

public class CancerBullet30 : ScriptableEnemyBullet<CancerBulletSystem3, EnemyBullet>
{
    const int BulletCount = 6;
    const float BulletSpacing = 360f / BulletCount;

    protected override float MaxLifetime => 0.25f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 8f;
        yield break;
    }

    public override void Destroy()
    {
        float r = Random.Range(0f, BulletSpacing);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = i * BulletSpacing + r;
            Vector3 pos = transform.position;

            SpawnBullet(1, z, pos, false).Fire();
        }

        base.Destroy();
    }
}