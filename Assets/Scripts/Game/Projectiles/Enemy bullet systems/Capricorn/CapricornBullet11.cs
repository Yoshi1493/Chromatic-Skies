using System.Collections;

public class CapricornBullet11 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(8f, 2f, 0.5f);
        StartCoroutine(this.RotateBy(30f, 4f));
    }
}