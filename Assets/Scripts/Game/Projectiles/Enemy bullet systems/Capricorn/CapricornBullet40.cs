using System.Collections;

public class CapricornBullet40 : EnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.RotateBy(15f, 5f));
        yield return this.LerpSpeed(0f, 2f, 2f);
    }
}