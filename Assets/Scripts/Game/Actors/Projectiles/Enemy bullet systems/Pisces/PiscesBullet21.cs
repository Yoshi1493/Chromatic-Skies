using System.Collections;

public class PiscesBullet21 : EnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(6f, 0f, 0.5f);

        StartCoroutine(this.LerpSpeed(0f, endSpeed, 3f));
        yield return this.HomeInOn(playerShip, 3f);
    }
}