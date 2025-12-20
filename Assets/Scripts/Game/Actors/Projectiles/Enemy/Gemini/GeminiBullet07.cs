using System.Collections;
using UnityEngine;

public class GeminiBullet07 : MinibossBullet
{
    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 2.5f, 0.5f);
    }

    //don't return to object pool; handled by Shooter
    public override void Destroy()
    {
        if (movementBehaviour != null)
        {
            StopCoroutine(movementBehaviour);
        }

        MoveSpeed = 0f;
    }
}