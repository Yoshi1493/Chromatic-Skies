using System.Collections;

public class SagittariusBullet51 : EnemyBullet
{
    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        yield return null;
    }
}