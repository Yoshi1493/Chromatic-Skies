using System.Collections;

public class GeminiBullet11 : EnemyBullet
{
    protected override float MaxLifetime => 8f; 

    protected override IEnumerator Move()
    {
        MoveSpeed = 2f;
        yield return null;
    }
}