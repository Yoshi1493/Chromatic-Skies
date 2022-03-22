using System.Collections;

public class VirgoBullet21 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 2.5f, 2.5f);
    }
}