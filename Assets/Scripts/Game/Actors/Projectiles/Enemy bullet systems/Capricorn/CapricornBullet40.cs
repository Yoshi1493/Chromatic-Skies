using System.Collections;
using static CoroutineHelper;

public class CapricornBullet40 : EnemyBullet
{
    protected override float MaxLifetime => 12.5f;

    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(4f);

        StartCoroutine(this.RotateBy(15f, 5f));
        yield return this.LerpSpeed(0f, 2f, 2f);
    }
}