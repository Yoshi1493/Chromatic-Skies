using System.Collections;
using UnityEngine;
using static MathHelper;

public class ScorpioBullet40 : EnemyBullet, ITimestoppable
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
        spriteRenderer.color = OriginalColour;
        StartCoroutine(ResumeMove());
    }

    public IEnumerator ResumeMove()
    {
        moveDirection = this.LookAt(playerShip);
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
        StartCoroutine(this.LerpSpeed(1f, 3f, 2f));
        yield return this.RotateBy(Random.Range(-30f, 30f) + 30f, 3f);
    }
}