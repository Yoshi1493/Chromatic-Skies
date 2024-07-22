using System.Collections;

public class ScorpioBullet21 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        moveDirection *= -1;
        yield return this.LerpSpeed(0f, 2f, 2f);
    }
}