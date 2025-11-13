using System.Collections;

public class TaurusBullet03 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        while (enabled)
        {
            StartCoroutine(this.RotateBy(45f, 0f));
            yield return this.LerpSpeed(4f, 0f, 1f);

            StartCoroutine(this.RotateBy(-45f, 0f));
            yield return this.LerpSpeed(4f, 0f, 1f);
        }
    }
}