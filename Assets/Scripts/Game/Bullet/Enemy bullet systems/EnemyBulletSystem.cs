using UnityEngine;

public abstract class EnemyBulletSystem : EnemyShooter
{
    protected override void Start()
    {
        base.Start();
        ownerShip.LoseLifeAction += OnLoseLife;
    }

    protected virtual void SpawnBullet(int bulletIndex, float zRotation, Vector3 offset)
    {
        var newBullet = EnemyBulletPool.Instance.Get(bulletIndex);
        newBullet.transform.SetPositionAndRotation(transform.position + offset, Quaternion.Euler(0, 0, zRotation));
        newBullet.gameObject.SetActive(true);
        newBullet.enabled = true;
    }

    protected virtual void OnLoseLife()
    {
        StopCoroutine(shootCoroutine);
        DestroyAllBullets<EnemyBullet>();
    }
}