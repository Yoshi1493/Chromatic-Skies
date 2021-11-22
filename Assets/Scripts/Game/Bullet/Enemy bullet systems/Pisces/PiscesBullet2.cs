using System.Collections;

public class PiscesBullet2 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 3f, 1f);
    }
}