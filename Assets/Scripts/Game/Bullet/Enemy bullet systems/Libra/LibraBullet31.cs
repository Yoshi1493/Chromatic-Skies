using System.Collections;

public class LibraBullet31 : EnemyBullet
{
    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;

        StartCoroutine(this.RotateBy(-30f, 1f));
        yield return this.LerpSpeed(3f, 2f, 1f, 0.5f);
    }
}