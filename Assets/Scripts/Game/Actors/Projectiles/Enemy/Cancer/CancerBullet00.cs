using System.Collections;
using UnityEngine;

public class CancerBullet00 : EnemyBullet
{
    protected override int NumCollisions => Physics2D.OverlapBox(transform.position, SpriteRenderer.size, transform.eulerAngles.z, contactFilter, collisionResults);

    protected override float MaxLifetime => 5f;
    
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(Random.Range(4f, 8f), 0f, 0.5f);
        yield return this.HomeInOn(playerShip, 0.5f);
        StartCoroutine(this.LerpSpeed(0f, 4f, 0.5f));
    }
}