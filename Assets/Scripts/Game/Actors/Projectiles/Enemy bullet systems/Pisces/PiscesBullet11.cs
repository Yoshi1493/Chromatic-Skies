using System.Collections;

public class PiscesBullet11 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(endSpeed * 3f, 0f, 0.5f);
        yield return this.LerpSpeed(0f, endSpeed, 1f);
    }
}