using System.Collections;

public class AquariusBullet00 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 2f, 1f);
    }
}