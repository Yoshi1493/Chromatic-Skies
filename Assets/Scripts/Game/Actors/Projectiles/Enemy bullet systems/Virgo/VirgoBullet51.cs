using System.Collections;
using UnityEngine;

public class VirgoBullet51 : ScriptableEnemyBullet<VirgoBulletSystem51, EnemyBullet>
{
    const int BulletCount = 5;
    const float BulletSpacing = 360f / BulletCount;

    protected override IEnumerator Move()
    {
        bool r = Random.value > 0.5f;

        for (int i = 0; i < BulletCount; i++)
        {
            float z = i * BulletSpacing;
            Vector3 pos = transform.position + (0.5f * transform.up.RotateVectorBy(z));

            var bullet = SpawnBullet(3, z, pos, false);
            bullet.transform.parent = transform.GetChild(0);
            bullet.StartCoroutine(bullet.RotateAround(this, MaxLifetime, 60f, r));
        }

        yield return this.LerpSpeed(2f, 3f, 1f);
    }
}