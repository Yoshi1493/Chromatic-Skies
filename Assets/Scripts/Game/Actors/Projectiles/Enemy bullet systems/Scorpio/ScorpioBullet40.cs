using System.Collections;
using UnityEngine;

public class ScorpioBullet40 : EnemyBullet, ITimestoppable
{
    new CircleCollider2D collider;

    protected override float MaxLifetime => 20f;

    #region Interface impl.

    public Color OriginalColour { get; set; }

    public void Stop()
    {
        StopAllCoroutines();

        SpriteRenderer.color = Color.white;
        MoveSpeed = 0f;
        collider.enabled = false;
    }

    public void Resume()
    {
        StartCoroutine(ResumeMove());
    }

    public IEnumerator ResumeMove()
    {
        moveDirection *= -1;
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

        OriginalColour = SpriteRenderer.color;
        collider.enabled = true;
    }

    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;
        yield return this.LerpSpeed(startSpeed, 0f, 0.5f);
        yield return this.LerpSpeed(0f, startSpeed * 0.5f, 3f);
    }
}