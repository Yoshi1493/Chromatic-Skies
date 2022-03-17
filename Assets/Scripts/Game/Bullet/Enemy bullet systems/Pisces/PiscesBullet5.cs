using System.Collections;

public class PiscesBullet5 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(6f, 2f, 3f);
    }
}