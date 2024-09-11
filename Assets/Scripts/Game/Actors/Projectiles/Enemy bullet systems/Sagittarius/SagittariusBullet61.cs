using System.Collections;
using static CoroutineHelper;

public class SagittariusBullet61 : EnemyBullet
{
    protected override float MaxLifetime => 20f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0f, 0.5f);
        yield return WaitForSeconds(0.5f);
        yield return this.LerpSpeed(0f, 1f, 1f);
    }

    protected override void Update()
    {
        base.Update();

        if (currentLifetime < MaxLifetime)
        {
            SpriteRenderer.color = projectileData.gradient.Evaluate(currentLifetime / MaxLifetime);
        }
    }
}