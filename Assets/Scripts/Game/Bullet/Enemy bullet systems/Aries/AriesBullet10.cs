using System.Collections;

public class AriesBullet10 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2f, 4f, 2f);
    }
}
