using System.Collections;

public class VirgoBullet31 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 4f, 1f);
    }
}