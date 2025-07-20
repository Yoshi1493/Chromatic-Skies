using System.Collections;

public class AquariusBullet03 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 2.5f, 1f);
    }
}