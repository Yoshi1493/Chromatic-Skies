using System.Collections;

public class SagittariusBullet41 : EnemyBullet
{
    protected override float MaxLifetime => 20f;

    protected override IEnumerator Move()
    {
        yield break;
    }
}