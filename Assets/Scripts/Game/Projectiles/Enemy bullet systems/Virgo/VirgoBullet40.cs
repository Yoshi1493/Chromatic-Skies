using System.Collections;

public class VirgoBullet40 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return null;

        StartCoroutine(this.LerpSpeed(1f, 4f, 1f));
        yield return this.RotateAround(ownerShip.transform.position, 1.5f, 270f, delay: 0.5f);
        yield return this.LerpSpeed(MoveSpeed, 4f, 1f);
    }
}