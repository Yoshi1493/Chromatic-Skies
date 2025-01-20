using System.Collections;

public class GeminiBullet22 : EnemyBullet
{
    protected override float MaxLifetime => 1f;

    protected override IEnumerator Move()
    {
        yield return null;
    }

    protected override void Update()
    {
        base.Update();
        SpriteRenderer.color = projectileData.gradient.Evaluate(currentLifetime);
    }
}