using System.Collections;

public class TaurusBullet56 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2f, 0f, 1f);

        StartCoroutine(this.RotateBy(90f, 0f));
        yield return this.LerpSpeed(4f, 0f, 1f);

        yield return this.RotateBy(-90f, 0f);
        MoveSpeed = 3f;
    }
}