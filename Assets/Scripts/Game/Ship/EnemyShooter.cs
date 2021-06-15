using System.Collections;

public class EnemyShooter : Shooter
{
    IEnumerator Start()
    {
        yield return CoroutineHelper.WaitForSeconds(3);
        SpawnBullet(0);
        yield return null;
    }

    protected override void SpawnBullet(int bulletIndex)
    {
        var newBullet = EnemyBulletPool.Instance.Get(bulletIndex);

        newBullet.transform.SetPositionAndRotation(transform.position, transform.rotation);
        newBullet.gameObject.SetActive(true);
    }
}