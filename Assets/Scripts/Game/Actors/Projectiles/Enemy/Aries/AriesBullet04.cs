using System.Collections;

public class AriesBullet04 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(-1f, 4f, 2f);
    }
}