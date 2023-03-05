using System.Collections;

public class AriesBullet20 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;

        yield return this.LerpSpeed(0f, 2f, 1f);
        yield return this.RotateAround(ownerShip, 1f, 180f);
        yield return this.LerpSpeed(MoveSpeed, endSpeed, 0.5f);
    }
}