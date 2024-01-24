using System.Collections;
using UnityEngine;
using static MathHelper;

public class ScorpioBullet20 : EnemyBullet, ITimestoppable
{
    protected override float MaxLifetime => 20f;

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
        StartCoroutine(ResumeMove());
    }

    public IEnumerator ResumeMove()
    {
        moveDirection = transform.up.RotateVectorBy(RandomAngleDeg);
        yield return this.LerpSpeed(0f, 2f, 2f);
    }

    #endregion

    protected override void OnEnable()
    {
        base.OnEnable();
        OriginalColour = spriteRenderer.color;
    }

    protected override IEnumerator Move()
    {
        yield break;
    }
}