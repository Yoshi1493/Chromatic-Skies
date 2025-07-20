using System.Collections;

public class AquariusBullet01 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2f, 4f, 1f);
    }
}