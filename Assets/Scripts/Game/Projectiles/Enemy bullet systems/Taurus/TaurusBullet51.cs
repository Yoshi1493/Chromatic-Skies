using System.Collections;

public class TaurusBullet51 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;
        yield return null;
    }
}