using System.Collections;

public class LibraBullet68 : EnemyBullet
{
    protected override float MaxLifetime => 9f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0f, 2f);
        spriteRenderer.color = projectileData.gradient.colorKeys[^1].color;
        yield return this.LerpSpeed(0f, 2f, 0.5f);
    }
}