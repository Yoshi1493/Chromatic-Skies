using System.Collections;

public class AquariusBullet21 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0f, 0.5f);
        StartCoroutine(this.LerpSpeed(0f, 3f, 2f));
    }
}