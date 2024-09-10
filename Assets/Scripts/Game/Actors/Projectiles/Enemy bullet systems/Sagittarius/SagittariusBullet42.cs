using System.Collections;
using UnityEngine;

public class SagittariusBullet42 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0.8f, 1.6f, 1f);
    }

    protected override void Update()
    {
        base.Update();

        if (currentLifetime <= 1f)
        {
            Color c = SpriteRenderer.color;
            c.a = currentLifetime;
            SpriteRenderer.color = c;
        }
    }
}