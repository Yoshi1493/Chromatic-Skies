using System.Collections;
using static CoroutineHelper;

public class GeminiBullet41 : EnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1f);
        StartCoroutine(this.LerpSpeed(0f, 1.8f, 2f));
        yield return this.RotateBy(30f, 4f);
    }
}