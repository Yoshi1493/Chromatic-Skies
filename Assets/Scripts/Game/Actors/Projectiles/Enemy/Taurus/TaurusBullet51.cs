using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBullet51 : ScriptableBossBullet<TaurusBossShooter53, BossLaser>
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed((TaurusBossShooter51.BulletSpawnRadius - 2f) * -2f, 0f, 1f);
        yield return WaitForSeconds(0.1f);

        SpawnSmallLasers();
        yield return WaitForSeconds(1f);

        SpawnLargeLaser();
        yield return WaitForSeconds(3f);

        yield return this.LerpSpeed(0f, -3f, 5f);
    }

    void SpawnSmallLasers()
    {
        for (int i = 0; i < 2; i++)
        {
            float z = transform.eulerAngles.z + 180f + ((i % 2 * 2 - 1) * (180f * (TaurusBossShooter51.BulletCount - 2) / TaurusBossShooter51.BulletCount / 2f));
            Vector3 pos = transform.position;

            SpawnBullet(0, z, pos, false).Fire(0f);
        }
    }

    void SpawnLargeLaser()
    {
        float z = transform.eulerAngles.z + 180f + 10f;
        Vector3 pos = transform.position;

        SpawnBullet(1, z, pos, false).Fire(1f);
    }
}