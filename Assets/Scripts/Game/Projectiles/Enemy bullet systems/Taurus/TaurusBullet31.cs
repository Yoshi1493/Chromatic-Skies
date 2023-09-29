using System.Collections;

public class TaurusBullet31 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return null;
    }
}