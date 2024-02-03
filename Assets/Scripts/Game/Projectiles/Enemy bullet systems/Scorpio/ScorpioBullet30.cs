using System.Collections;

public class ScorpioBullet30 : EnemyBullet
{
    protected override float MaxLifetime => ScorpioBulletSystem3.BigBulletRotationDuration;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0f, MaxLifetime);
    }

    protected override void Update()
    {
        base.Update();
        spriteRenderer.color = projectileData.gradient.Evaluate(currentLifetime / MaxLifetime);
    }
}