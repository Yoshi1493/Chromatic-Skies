using System.Collections;

public class TaurusBullet58 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 2.5f;
        yield return null;
    }
}