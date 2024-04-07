using System.Collections;

public class AquariusBullet51 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(10f, 2f, 1f);
    }
}