using System.Collections;

public class CapricornBullet11 : EnemyBullet
{
    protected override float MaxLifetime => 12f;

    protected override IEnumerator Move()
    {
        yield return null;
    }
}