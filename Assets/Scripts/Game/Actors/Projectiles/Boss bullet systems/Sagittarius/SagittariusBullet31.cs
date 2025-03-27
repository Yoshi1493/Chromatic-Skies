using System.Collections;

public class SagittariusBullet31 : BossBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return null;
    }
}