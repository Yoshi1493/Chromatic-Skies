using System.Collections;

public class AquariusBullet11 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpSpeed(0f, 4f, 2f));
        yield return this.RotateBy(180f, MaxLifetime * 0.5f);
    }
}