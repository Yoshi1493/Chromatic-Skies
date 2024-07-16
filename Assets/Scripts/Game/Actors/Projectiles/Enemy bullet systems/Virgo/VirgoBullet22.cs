using System.Collections;

public class VirgoBullet22 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 2.5f, 1f);
    }
}