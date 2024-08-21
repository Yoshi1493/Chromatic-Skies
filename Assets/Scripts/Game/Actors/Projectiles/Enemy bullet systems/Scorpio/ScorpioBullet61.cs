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

            spriteRenderer.color = Color.white;
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
        yield return this.LerpSpeed(0f, -2f, 2f);
        collider.enabled = true;
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
        hasStopped = false;
    }

    protected override void Update()
    {
        base.Update();

        if (currentLifetime <= 0.5f)
        {
            Color c = spriteRenderer.color;
            c.a = Mathf.Clamp01(currentLifetime * 2f);
            spriteRenderer.color = c;
        }
    }

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(1f, 2f, 1f);
    }
}