using System.Collections;
using UnityEngine;

public class LibraBullet40 : ScriptableEnemyBullet<LibraBulletSystem4, EnemyBullet>
{
    const int BulletCount = 3;
    const float BulletSpacing = 360f / BulletCount;

    protected override float MaxLifetime => 2.5f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0f, 0.5f);
    }

    public override void Destroy()
    {
        float r = Random.Range(0f, BulletSpacing);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = (i * BulletSpacing) + r;
            Vector3 pos = transform.position;

            SpawnBullet(1, z, pos, false).Fire();
        }

        base.Destroy();
    }
}