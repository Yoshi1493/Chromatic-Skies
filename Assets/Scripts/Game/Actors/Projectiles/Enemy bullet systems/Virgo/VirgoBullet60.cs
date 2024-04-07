using System.Collections;

public class VirgoBullet60 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 3f, 0.5f);
    }
}