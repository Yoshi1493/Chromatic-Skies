using System.Collections;
using UnityEngine;
using static MathHelper;

public class ScorpioBullet20 : EnemyBullet, ITimestoppable
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
        moveDirection = transform.up.RotateVectorBy(RandomAngleDeg);
        yield return this.LerpSpeed(0f, 2f, 2f);
    }

    #endregion

    protected override void Awake()
    {
        base.Awake();
        collider = GetComponent<CircleCollider2D>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        OriginalColour = spriteRenderer.color;
        collider.enabled = true;
    }

    protected override IEnumerator Move()
    {
        yield return this.RotateBy(Random.Range(-30f, 30f), 3f);
    }
}