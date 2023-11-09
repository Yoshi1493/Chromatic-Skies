using System;
using System.Collections;

public class PiscesBullet20 : EnemyBullet
{
    public event Action<PiscesBullet20> DestroyAction;

    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 2.5f;
        yield return null;
    }

    public override void Destroy()
    {
        DestroyAction?.Invoke(this);
        base.Destroy();
    }
}