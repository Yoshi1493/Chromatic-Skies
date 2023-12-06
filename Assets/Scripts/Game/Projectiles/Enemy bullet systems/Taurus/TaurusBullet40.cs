using System.Collections;
using static CoroutineHelper;

public class TaurusBullet40 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        yield return null;
    }
}