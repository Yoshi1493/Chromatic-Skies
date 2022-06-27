using System.Collections;

public class VirgoBullet41 : EnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.RotateBy(-60f, 5f));
        yield return this.LerpSpeed(0f, 3f, 2f);
    }
}