using System.Collections;

public class CapricornBullet51 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.RotateAround(ownerShip, 1f, 60f, false);
        yield return this.LerpSpeed(MoveSpeed, 3f, 1f);
    }
}