using System.Collections;
using UnityEngine;

public class SagittariusBullet60 : EnemyBullet
{
    const float BulletSpawnRadius = 0.5f;

    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 2f, 1f);
    }

    protected override void Update()
    {
        base.Update();

        if (currentLifetime - 1f <= 4f)
        {
            Color c = SpriteRenderer.color;
            c.a = 1f - ((currentLifetime - 1f) / 4f);
            SpriteRenderer.color = c;
        }
    }
}