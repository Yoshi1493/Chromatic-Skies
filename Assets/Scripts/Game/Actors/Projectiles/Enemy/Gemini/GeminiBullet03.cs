using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBullet03 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return WaitForSeconds(1.5f);
        yield return this.LerpSpeed(0f, 3f, 1f);
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