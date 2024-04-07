using System.Collections;
using static CoroutineHelper;

public class AquariusBullet30 : EnemyBullet
{
    protected override float MaxLifetime => 16f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;

        yield return WaitForSeconds(1f);
        yield return StartCoroutine(this.LerpSpeed(0f, -2f, 3f));
    }
}