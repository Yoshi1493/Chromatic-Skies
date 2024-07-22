using System.Collections;
using UnityEngine;

public class CancerBullet31 : ScriptableEnemyBullet<CancerBulletSystem3, EnemyBullet>
{
    const int BulletCount = 3;
    const float BulletSpacing = 360f / BulletCount;

    protected override float MaxLifetime => 3f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0.5f, 0f, 0.5f);
    }

    public override void Destroy()
    {
        for (int i = 0; i < BulletCount; i++)
        {
            float z = (i * BulletSpacing) + transform.eulerAngles.z;
            Vector3 pos = transform.position;

            SpawnBullet(3, z, pos, false).Fire();
        }

        base.Destroy();
    }
}