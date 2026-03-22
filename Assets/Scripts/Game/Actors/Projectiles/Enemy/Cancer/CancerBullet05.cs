using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBullet05 : MinibossBullet
{
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, SpriteRenderer.size, transform.eulerAngles.z, collisionResults, CollisionMask);

    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1f);

        yield return this.HomeInOn(playerShip, 3f, 1f);
        yield return this.LerpSpeed(0f, 2f, 2f);
    }

    protected override void Update()
    {
        base.Update();

        Color c = SpriteRenderer.color;

        if (currentLifetime <= 1f)
        {
            c.a = currentLifetime;
        }
        else if (currentLifetime <= 2f)
        {
            c = projectileData.gradient.Evaluate(currentLifetime - 1f);
        }

        SpriteRenderer.color = c;
    }
}