using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBullet41 : ScriptableEnemyBullet<SagittariusBulletSystem41, EnemyBullet>
{
    protected override IEnumerator Move()
    {
        float endSpeed = 0.8f;
        float delay = 0.5f;

        yield return this.LerpSpeed(0f, endSpeed, 1f);

        float currentLerpTime = 0f;
        float totalLerpTime = ((SagittariusBulletSystem41.BulletSpawnRadius - (endSpeed / 2f)) / endSpeed) - delay;

        while (currentLerpTime < totalLerpTime)
        {
            Color c = SpriteRenderer.color;
            c.a = 1f - (currentLerpTime / totalLerpTime);
            SpriteRenderer.color = c;

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }

        Destroy();
    }

    public override void Destroy()
    {
        float z = transform.eulerAngles.z + 180f;
        Vector3 pos = transform.position;

        SpawnBullet(2, z, pos, false).Fire();

        base.Destroy();
    }
}