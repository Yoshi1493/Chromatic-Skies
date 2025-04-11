using System.Collections;

public class PiscesBullet20 : BossBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 2.5f;
        yield return null;
    }
}