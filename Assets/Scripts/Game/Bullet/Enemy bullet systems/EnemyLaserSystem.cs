using UnityEngine;

public abstract class EnemyLaserSystem : EnemyShooter
{
    protected override void Start()
    {
        base.Awake();
        ownerShip.LoseLifeAction += OnLoseLife;
    }

    protected Laser SpawnLaser(int laserIndex, float zRotation, Vector2 offset)
    {
        Laser newLaser = EnemyLaserPool.Instance.Get(laserIndex);

        newLaser.transform.SetPositionAndRotation(ShipPosition + offset, Quaternion.Euler(0, 0, zRotation));

        newLaser.gameObject.SetActive(true);
        newLaser.enabled = true;

        return newLaser;
    }

    void OnLoseLife()
    {
        StopCoroutine(shootCoroutine);
        DestroyAllProjectiles<Laser>();
    }
}