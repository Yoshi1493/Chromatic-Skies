using System.Collections;

public class AquariusBullet08 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 4f, 1f);
    }
}