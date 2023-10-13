using System.Collections;

public class AquariusBullet42 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(4f, endSpeed, 1f);
    }
}