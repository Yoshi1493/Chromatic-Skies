using System.Collections;

public class CancerBullet43 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 2f;
        yield return null;
    }
}