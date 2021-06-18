using System.Collections;

public class EnemyShooter : Shooter
{
    protected override void SpawnBullet(int index)
    {
        var newBullet = EnemyBulletPool.Instance.Get(index);

        newBullet.transform.SetPositionAndRotation(spawnPositions[0].position, spawnPositions[0].rotation);
        newBullet.gameObject.SetActive(true);
    }
}