using System.Collections;

public class GeminiBullet40 : EnemyBullet
{
    protected override float MaxLifetime => 2f;

    protected override IEnumerator Move()
    {
        yield break;
    }

    protected override void Update()
    {
        base.Update();

        float t = currentLifetime / MaxLifetime;
        SpriteRenderer.color = projectileData.gradient.Evaluate(t);
    }
}