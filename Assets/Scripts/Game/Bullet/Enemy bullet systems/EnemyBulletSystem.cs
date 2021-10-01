using UnityEngine;

public abstract class EnemyBulletSystem : EnemyShooter
{
    protected override void Start()
    {
        base.Start();
        ownerShip.LoseLifeAction += OnLoseLife;
    }

    protected virtual EnemyBullet SpawnBullet(int bulletIndex, float zRotation, Vector3 position, bool asLocalPosition = true)
    {
        EnemyBullet newBullet = EnemyBulletPool.Instance.Get(bulletIndex);

        newBullet.transform.SetPositionAndRotation(position + (asLocalPosition ? transform.position : Vector3.zero), Quaternion.Euler(0, 0, zRotation));

        newBullet.gameObject.SetActive(true);
        newBullet.enabled = true;

        //newBullet.name += $"{newBullet.transform.GetSiblingIndex()}";

        return newBullet;
    }

    protected virtual void OnLoseLife()
    {
        StopCoroutine(shootCoroutine);
        DestroyAllBullets<EnemyBullet>();
    }
}