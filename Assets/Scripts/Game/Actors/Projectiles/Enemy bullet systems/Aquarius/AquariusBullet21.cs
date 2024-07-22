using System.Collections;

public class AquariusBullet21 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 2f, 1f);
    }
}