using System.Collections;

public class VirgoBullet02 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0f, 1f);
    }
}