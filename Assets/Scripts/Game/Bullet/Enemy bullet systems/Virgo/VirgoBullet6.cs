using System.Collections;

public class VirgoBullet6 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 3f, 1f, 4.4f);
    }
}