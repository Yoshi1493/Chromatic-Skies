using System.Collections;

public class AriesBullet3 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return null;

        StartCoroutine(this.LerpSpeed(1f, 3f, 1f));
        StartCoroutine(this.RotateAround(ownerShip, 2f, 180f, delay: 0.5f));
    }
}