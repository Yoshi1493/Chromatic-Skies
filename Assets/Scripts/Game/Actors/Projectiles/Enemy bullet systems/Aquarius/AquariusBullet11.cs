using System;
using System.Collections;

public class AquariusBullet11 : EnemyBullet
{
    public event Action<AquariusBullet11> DestroyAction;

    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, -3f, 2f);
        yield return this.LerpSpeed(-3f, -1.5f, 4f);
    }

    public override void Destroy()
    {
        DestroyAction?.Invoke(this);
        base.Destroy();
    }
}