using System.Collections;

public class CapricornBullet22 : EnemyBullet
{
    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 0f, 0.5f);
        StartCoroutine(this.HomeInOn(playerShip, 1f));
        StartCoroutine(this.LerpSpeed(0f, 5f, 1f));
    }
}