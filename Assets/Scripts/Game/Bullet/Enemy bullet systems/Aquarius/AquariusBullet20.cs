using System.Collections;

public class AquariusBullet20 : EnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        StartCoroutine(this.RotateBy(90f, 3f));
        yield return this.LerpSpeed(0f, 2f, 2f);
    }
}