using System.Collections;

public class CapricornBullet50 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;

        yield return this.LerpSpeed(0f, 3f, 0.5f);
        yield return this.RotateAround(ownerShip, 1f, 180f);
        StartCoroutine(this.LerpSpeed(MoveSpeed, endSpeed, 1f));
    }
}