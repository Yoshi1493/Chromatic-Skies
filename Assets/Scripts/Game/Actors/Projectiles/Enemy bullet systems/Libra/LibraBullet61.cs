using System.Collections;

public class LibraBullet61 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpSpeed(4f, 0f, 1f));
        yield return this.RotateBy(90f, 1f);
    }
}