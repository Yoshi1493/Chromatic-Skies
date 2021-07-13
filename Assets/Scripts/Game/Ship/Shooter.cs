using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    protected Ship ownerShip;
    protected Vector3 ShipPosition => ownerShip.transform.position;

    [SerializeField] protected FloatReference shootingSpeed;
    protected float ShootingSpeed => shootingSpeed.CurrentValue;
    protected virtual float ShootingCooldown => ShootingSpeed == 0 ? 0 : 1 / ShootingSpeed;

    protected List<Transform> spawnPositions = new List<Transform>();

    protected virtual void Awake()
    {
        ownerShip = GetComponentInParent<Ship>();

        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPositions.Add(transform.GetChild(i));
        }
    }

    protected abstract void SpawnBullet(int bulletIndex, int spawnPositionIndex);

    protected void DestroyAllBullets<T>() where T : Bullet
    {
        T[] bullets = FindObjectsOfType<T>();

        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i].Destroy();
        }
    }
}