using System;
using System.Collections;

public class LibraBullet41 : EnemyBullet
{
    public event Action<LibraBullet41> DestroyAction;

    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 2f, 2f);
    }

    public override void Destroy()
    {
        DestroyAction?.Invoke(this);
        base.Destroy();
    }
}