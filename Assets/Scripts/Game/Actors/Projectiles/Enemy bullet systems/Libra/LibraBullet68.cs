using System.Collections;

public class LibraBullet68 : EnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0f, 2f);

        SpriteRenderer.color = projectileData.gradient.colorKeys[^1].color;
        yield return this.LerpSpeed(0f, 2f, 1f);
        yield return this.LerpSpeed(2f, -2f, 1f);
    }
}