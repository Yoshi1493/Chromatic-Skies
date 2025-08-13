using System.Collections;

public class CapricornBullet06 : EnemyBullet
{
    protected override float MaxLifetime => 8f;
    protected override IEnumerator Move()
    {
        yield return null;
    }
}