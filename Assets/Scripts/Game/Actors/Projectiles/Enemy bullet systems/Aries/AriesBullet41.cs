using System.Collections;

public class AriesBullet41 : EnemyBullet
{
    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 4f, 2f);
    }
}