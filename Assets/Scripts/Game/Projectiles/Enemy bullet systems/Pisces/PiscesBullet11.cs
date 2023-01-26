using System.Collections;

public class PiscesBullet11 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(endSpeed, 0f, 0.5f);

        StartCoroutine(this.RotateBy(60f, 4f, false));
        yield return this.LerpSpeed(0f, endSpeed, 1f);
    }
}