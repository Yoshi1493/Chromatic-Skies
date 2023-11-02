using System.Collections;

public class CapricornBullet31 : EnemyBullet
{
    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        yield return null;
    }
}