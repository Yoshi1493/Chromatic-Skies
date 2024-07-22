using System.Collections;

public class LibraBullet32 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0.5f, 0f, 1f);
        StartCoroutine(this.RotateBy(60f, 4f, false));
        yield return this.LerpSpeed(0f, 2.5f, 1f);
    }
}