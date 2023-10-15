using System.Collections;

public class CapricornBullet50 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        StartCoroutine(this.RotateBy(45f, 5f));

        yield return this.LerpSpeed(endSpeed, 0f, 0.5f);
        yield return this.LerpSpeed(0f, endSpeed, 1f);
    }
}