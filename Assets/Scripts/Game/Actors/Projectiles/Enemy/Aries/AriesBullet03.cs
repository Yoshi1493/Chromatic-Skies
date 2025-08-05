using System.Collections;

public class AriesBullet03 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 2f, 1.5f);
    }
}