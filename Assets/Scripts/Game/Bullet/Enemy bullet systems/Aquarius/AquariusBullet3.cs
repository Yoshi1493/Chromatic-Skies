using System.Collections;

public class AquariusBullet3 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return this.LerpSpeed(0f, 3f, 2f, 1f);
    }
}