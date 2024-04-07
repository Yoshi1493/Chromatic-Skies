using System.Collections;

public class AquariusBullet62 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(1f, 3f, 2f);
    }
}