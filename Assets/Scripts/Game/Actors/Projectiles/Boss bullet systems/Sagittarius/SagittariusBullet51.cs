using System.Collections;

public class SagittariusBullet51 : BossBullet
{
    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        yield return null;
    }
}