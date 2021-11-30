using System.Collections;

public class AquariusBullet5 : EnemyBullet
{
    protected override float MaxLifetime => 16f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return StartCoroutine(this.LerpSpeed(0f, -2f, 2f, 2.5f));
    }
}