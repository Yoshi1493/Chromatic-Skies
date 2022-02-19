using System.Collections;

public class LeoBullet1 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            yield return this.LerpSpeed(3f, 0f, 0.5f);
            yield return this.LerpSpeed(0f, 3f, 0.5f);
        }
    }
}