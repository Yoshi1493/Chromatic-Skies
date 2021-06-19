public class EnemyShooter : Shooter
{
    protected override void SpawnBullet(int bulletIndex, int spawnPositionIndex)
    {
        if (spawnPositionIndex >= spawnPositions.Count) return;

        var newBullet = EnemyBulletPool.Instance.Get(bulletIndex);

        newBullet.transform.SetPositionAndRotation(spawnPositions[spawnPositionIndex].position, spawnPositions[spawnPositionIndex].rotation);
        newBullet.gameObject.SetActive(true);
    }
}