using System.Collections;

public class PiscesBullet11 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(0f, (2f + endSpeed), 1f);
    }
}