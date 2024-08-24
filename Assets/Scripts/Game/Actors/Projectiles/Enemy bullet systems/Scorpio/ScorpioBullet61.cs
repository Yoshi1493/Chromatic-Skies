using System.Collections;
using UnityEngine;

public class ScorpioBullet61 : EnemyBullet, ITimestoppable
{
    new CircleCollider2D collider;
    bool hasStopped;

    protected override float MaxLifetime => 20f;

    #region Interface impl.

    public Color OriginalColour { get; set; }

    public void Stop()
    {
        if (!hasStopped)
        {
            StopAllCoroutines();

            SpriteRenderer.color = Color.white;
            MoveSpeed = 0f;
            collider.enabled = false;
            hasStopped = true;
        }
    }

    public void Resume()
    {
        StartCoroutine(ResumeMove());
    }

    public IEnumerator ResumeMove()
    {
        collider.enabled = true;
        yield return this.LerpSpeed(0f, -2f, 2f);
    }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        collider = GetComponent<CircleCollider2D>();
        collider.enabled = false;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        OriginalColour = SpriteRenderer.color;
        collider.enabled = true;
        hasStopped = false;
    }

    protected override void Update()
    {
        base.Update();

        if (currentLifetime <= 0.5f)
        {
            Color c = SpriteRenderer.color;
            c.a = Mathf.Clamp01(currentLifetime * 2f);
            SpriteRenderer.color = c;
        }
    }

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0.5f, 0.5f);
    }

    public override void Destroy()
    {
        collider.enabled = false;
        base.Destroy();
    }
}