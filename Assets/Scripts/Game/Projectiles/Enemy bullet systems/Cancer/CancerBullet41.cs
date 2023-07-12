using System.Collections;
using UnityEngine;

public class CancerBullet41 : ScriptableEnemyBullet<CancerBulletSystem41, EnemyBullet>
{
    const int BulletCount = 2;
    const float BulletSpacing = 45f;

    protected override float MaxLifetime => 1f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0f, MaxLifetime);
    }

    public override void Destroy()
    {
        float r = transform.eulerAngles.z + 180f;

        for (int i = 0; i < BulletCount; i++)
        {
            float z = ((i - ((BulletCount - 1) / 2f)) * BulletSpacing) + r;
            Vector3 pos = transform.position;

            SpawnBullet(2, z, pos, false).Fire();
        }

        base.Destroy();
    }
}