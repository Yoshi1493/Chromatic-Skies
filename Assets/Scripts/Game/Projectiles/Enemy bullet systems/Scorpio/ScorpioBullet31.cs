using System.Collections;

public class ScorpioBullet31 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 2f, 1f);
    }
}