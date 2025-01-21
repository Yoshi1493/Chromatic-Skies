using System.Collections;

public class GeminiBullet22 : EnemyBullet
{
    protected override float MaxLifetime => 1.5f;

    protected override IEnumerator Move()
    {
        yield return null;
    }

    protected override void Update()
    {
        base.Update();

        float t = currentLifetime / MaxLifetime;
        SpriteRenderer.color = projectileData.gradient.Evaluate(t);
    }
}