using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBullet20 : ScriptableEnemyBullet<CancerBulletSystem2, EnemyBullet>
{
    const float ShootingCooldown = 0.1f;

    protected override float MaxLifetime => 9f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2.5f, 0f, 0.5f);

        StartCoroutine(this.RotateBy(180f, MaxLifetime, true));
        yield return this.LerpSpeed(0f, 2.5f, 0.5f);

        while (currentLifetime < 5f)
        {
            int b = Random.value * 3 <= 1 ? 1 : 2;
            Vector3 o = 0.5f * Random.insideUnitCircle;
            float z = transform.eulerAngles.z - (Mathf.Sign(o.x) * 90f);
            Vector3 pos = transform.position + o;

            SpawnBullet(b, z, pos, false).Fire();
            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}