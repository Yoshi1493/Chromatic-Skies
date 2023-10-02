using System.Collections;

public class TaurusBullet56 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 2.5f;
        yield return null;
    }
}