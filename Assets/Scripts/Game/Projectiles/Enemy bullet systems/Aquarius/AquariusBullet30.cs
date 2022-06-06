using System.Collections;

public class AquariusBullet30 : EnemyBullet
{
    protected override float MaxLifetime => 18f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return StartCoroutine(this.LerpSpeed(0f, -3f, 4f, 2f));
    }
}