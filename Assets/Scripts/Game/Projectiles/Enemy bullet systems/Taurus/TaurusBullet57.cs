using System.Collections;

public class TaurusBullet57 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 2.5f;
        yield return null;
    }
}