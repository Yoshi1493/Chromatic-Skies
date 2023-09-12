using System.Collections;

public class GeminiBullet22 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        StartCoroutine(this.RotateBy(30f, 1f, false));
        yield return this.LerpSpeed(0f, 3f, 0.5f);
        yield return this.LerpSpeed(3f, 0f, 0.5f);

        StartCoroutine(this.RotateBy(90f, 3f, true));
        yield return this.LerpSpeed(0f, 2f, 1.5f);
    }
}