using System.Collections;

public class VirgoBullet09 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 2f, 2f);
    }
}