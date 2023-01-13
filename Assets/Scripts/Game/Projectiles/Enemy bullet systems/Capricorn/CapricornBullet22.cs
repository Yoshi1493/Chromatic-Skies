using System.Collections;

public class CapricornBullet22 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(5f, 1f, 0.5f);
        StartCoroutine(this.HomeInOn(playerShip, 1f));
        StartCoroutine(this.LerpSpeed(1f, 2f, 1f));
    }
}