using System.Collections;

public class TaurusBullet56 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 2.5f;
        yield return null;
    }
}