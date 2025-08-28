using System.Collections;

public class PiscesBullet02 : EnemyBullet
{
    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 4f;
        yield return null;
    }
}