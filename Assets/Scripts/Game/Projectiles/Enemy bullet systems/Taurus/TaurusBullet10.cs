using System.Collections;

public class TaurusBullet10 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;

        yield return this.LerpSpeed(0f, startSpeed, 0.5f);
        yield return this.LerpSpeed(MoveSpeed, 0f, 0.5f);

        yield return this.GraduallyLookAt(playerShip.transform.position, 1f);
        StartCoroutine(this.LerpSpeed(0f, 2f, 0.5f, 0.5f));
    }
}