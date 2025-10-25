using System.Collections;

public class CancerBullet03 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2f, 0f, 0.5f);
    }
}