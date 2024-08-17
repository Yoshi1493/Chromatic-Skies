using System.Collections;

public class ScorpioBullet52 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        spriteRenderer.color = projectileData.gradient.colorKeys[^1].color;
        yield return this.LerpSpeed(0f, 2f, 1f);
    }
}