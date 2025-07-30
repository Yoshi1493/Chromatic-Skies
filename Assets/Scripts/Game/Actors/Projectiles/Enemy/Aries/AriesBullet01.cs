using System.Collections;

public class AriesBullet01 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0f, 0.5f);
    }
}