using System.Collections;
using UnityEngine;

public class ScorpioBullet20 : EnemyBullet, ITimestoppable
{
    #region Interface impl.

    public Color OriginalColour { get; set; }

    public void Stop()
    {
        StopAllCoroutines();

        MoveSpeed = 0f;
        spriteRenderer.color = Color.white;
    }

    public void Resume()
    {
        spriteRenderer.color = OriginalColour;
    }

    #endregion

    protected override void OnEnable()
    {
        base.OnEnable();
        OriginalColour = spriteRenderer.color;
    }

    protected override IEnumerator Move()
    {
        yield return null;
    }
}