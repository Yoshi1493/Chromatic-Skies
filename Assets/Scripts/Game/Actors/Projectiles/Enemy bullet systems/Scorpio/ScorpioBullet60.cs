using System.Collections;
using UnityEngine;

public class ScorpioBullet60 : EnemyBullet, ITimestoppable
{
    new CircleCollider2D collider;

    protected override float MaxLifetime => 20f;

    #region Interface impl.

    public Color OriginalColour { get; set; }

    public void Stop()
    {
        StopAllCoroutines();

        spriteRenderer.color = Color.white;
        MoveSpeed = 0f;
        collider.enabled = false;
    }

    public void Resume()
    {
        StartCoroutine(ResumeMove());
    }

    public IEnumerator ResumeMove()
    {
        yield return null;
        //implement
    }

    #endregion

    protected override void Awake()
    {
        base.Awake();
        collider = GetComponent<CircleCollider2D>();
    }

    protected override IEnumerator Move()
    {
        yield return null;
    }
}