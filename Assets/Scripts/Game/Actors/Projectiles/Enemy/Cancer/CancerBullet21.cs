using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class CancerBullet21 : ScriptableBossBullet<CancerBossShooter21, BossBullet>
{
    const int BulletCount = 6;
    const float BulletSpacing = 360f / BulletCount;

    [SerializeField] ProjectileObject bulletData;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpSpeed(0f, 5f, 0.4f));

        for (int i = 0; i < 20; i++)
        {
            float r = Random.Range(120f, 180f) * PositiveOrNegativeOne;
            yield return this.RotateBy(r, 0f);
            yield return WaitForSeconds(0.02f);
        }

        SpawnBullets();
        Destroy();
    }

    void SpawnBullets()
    {
        float r = playerShip.transform.position.GetRotationDifference(transform.position);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = (i * BulletSpacing) + r;
            Vector3 pos = transform.position;

            SpawnBullet(2, z, pos, false).Fire();
        }
    }
}