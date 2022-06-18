using System.Collections;

public class VirgoBullet11 : EnemyBullet
{
    protected override float MaxLifetime => 7f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 1f, 1f);
        StartCoroutine(this.LerpSpeed(1f, 2.5f, 1f));
    }
}