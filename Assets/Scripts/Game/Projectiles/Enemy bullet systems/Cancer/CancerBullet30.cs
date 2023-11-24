using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBullet30 : ScriptableEnemyBullet<CancerBulletSystem3, EnemyBullet>
{
    const float ShootingCooldown = 0.1f;

    protected override float MaxLifetime => 9f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2.5f, 1f, 0.5f);
        yield return this.LerpSpeed(1f, 2.5f, 0.5f);

        while (currentLifetime < 5f)
        {
            int b = Random.Range(1, 3);
            Vector3 r = 0.5f * Random.insideUnitCircle;
            float z = transform.eulerAngles.z - (Mathf.Sign(r.x) * 90f);
            Vector3 pos = transform.position + r;

            SpawnBullet(b, z, pos, false).Fire();
            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}