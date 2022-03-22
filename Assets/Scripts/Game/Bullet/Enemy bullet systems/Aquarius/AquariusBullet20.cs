using System.Collections;

public class AquariusBullet20 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return this.LerpSpeed(0f, 3f, 2f, 1f);
    }
}