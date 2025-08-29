using System.Collections;

public class PiscesBullet05 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0.5f, 0.5f);
        yield return this.LerpSpeed(0.5f, 4f, 2f);
    }
}