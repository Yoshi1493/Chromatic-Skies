using System;
using System.Collections;

public class CapricornBullet60 : EnemyBullet
{
    public event Action<CapricornBullet60> DestroyAction;

    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 3f, 2f);
    }

    public override void Destroy()
    {
        DestroyAction?.Invoke(this);
        base.Destroy();
    }
}