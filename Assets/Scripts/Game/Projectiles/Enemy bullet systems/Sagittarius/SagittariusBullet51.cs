using System.Collections;

public class SagittariusBullet51 : EnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        yield break;
    }
}