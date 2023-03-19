using System.Collections;
using UnityEngine;

public class CancerBullet30 : ScriptableEnemyBullet<CancerBulletSystem3, EnemyBullet>
{
    const int BulletCount = 18;
    const float ArcHalfWidth = 45f;
    protected override float MaxLifetime => 0.25f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 8f;
        yield break;
    }

    public override void Destroy()
    {
        MoveSpeed = 0f;
        float r = transform.eulerAngles.z + 180f;

        for (int i = 0; i < BulletCount; i++)
        {
            float z = r + Random.Range(-ArcHalfWidth, ArcHalfWidth);
            Vector3 pos = transform.position;

            SpawnBullet(1, z, pos, false).Fire();
        }

        base.Destroy();
    }
}