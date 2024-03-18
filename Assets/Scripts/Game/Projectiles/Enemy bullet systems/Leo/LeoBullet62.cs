using System.Collections;

public class LeoBullet62 : ScriptableEnemyBullet<LeoBulletSystem62, EnemyBullet>
{
    protected override IEnumerator Move()
    {
        StartCoroutine(this.LerpSpeed(0f, 2f, 2f));
        yield return this.RotateBy(45f, 5f);
    }
}