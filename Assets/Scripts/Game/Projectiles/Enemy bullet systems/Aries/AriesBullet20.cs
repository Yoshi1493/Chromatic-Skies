using System.Collections;

public class AriesBullet20 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, 2f, 1f);
        yield return this.RotateAround(ownerShip, 1f, 180f);
        yield return this.LerpSpeed(MoveSpeed, 2.5f, 0.5f);
    }
}