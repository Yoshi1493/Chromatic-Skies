using System.Collections;

public class AquariusBullet11 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, -3f, 2f);
    }
}