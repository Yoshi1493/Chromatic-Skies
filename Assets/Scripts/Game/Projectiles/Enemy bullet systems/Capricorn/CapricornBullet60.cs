using System.Collections;

public class CapricornBullet60 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;

        yield return this.LerpSpeed(0f, 3f, 0.5f);
        yield return this.RotateAround(ownerShip, 1f, 180f);

        yield return this.LerpSpeed(MoveSpeed, endSpeed * 2f, 0.5f);
        yield return this.LerpSpeed(endSpeed * 2f, endSpeed, 0.5f);
    }
}