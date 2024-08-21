using System.Collections;
using UnityEngine;

public class ScorpioBullet62 : EnemyBullet
{
    protected override float MaxLifetime => Mathf.Infinity;

    bool clockwise;
    public void Reverse() => clockwise = !clockwise;

    protected override IEnumerator Move()
    {
        yield return this.TransformRotateAround(EnemyMovementBehaviour.originalPosition, MaxLifetime, 6f);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        clockwise = true;
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
}