using System.Collections;

public class TaurusBullet30 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 2f;
        yield return null;
    }
}