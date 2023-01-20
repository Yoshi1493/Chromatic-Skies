using System.Collections;

public class TaurusBullet51 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 2f, 2f);
        //StartCoroutine(this.LerpSpeed(0f, 2f, 2f));
        //yield return this.RotateBy(30f, 3f, Random.value > 0.5f, 1f);
    }
}