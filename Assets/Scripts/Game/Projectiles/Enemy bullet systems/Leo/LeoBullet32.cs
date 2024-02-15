using System.Collections;

public class LeoBullet32 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpSpeed(2.5f, 1.5f, 2f));
        yield return this.RotateBy(90f, MaxLifetime);
    }
}