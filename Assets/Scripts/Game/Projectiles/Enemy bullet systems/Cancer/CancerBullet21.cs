using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CancerBullet21 : ScriptableEnemyBullet<CancerBulletSystem21, EnemyBullet>
{
    const int BulletCount = 6;
    const float BulletSpacing = 360f / BulletCount;

    [SerializeField] ProjectileObject bulletData;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpSpeed(0f, 5f, 0.5f));

        for (int i = 0; i < 10; i++)
        {
            float r = Random.Range(120f, 180f) * PositiveOrNegativeOne;
            yield return this.RotateBy(r, 0f);
            yield return WaitForSeconds(0.05f);
        }

        Destroy();
    }

    public override void Destroy()
    {
        float r = transform.eulerAngles.z;

        for (int i = 0; i < BulletCount; i++)
        {
            float z = (i * BulletSpacing) + r;
            Vector3 pos = transform.position;

            SpawnBullet(2, z, pos, false).Fire();
        }

        base.Destroy();
    }
}