using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    [SerializeField] protected FloatReference shootingSpeed;

    protected float ShootingSpeed => shootingSpeed.CurrentValue;
    protected virtual float ShootingCooldown => ShootingSpeed == 0 ? 0 : 1 / ShootingSpeed;

    protected List<Transform> spawnPositions = new List<Transform>();

    protected virtual void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPositions.Add(transform.GetChild(i));
        }
    }

    protected abstract void SpawnBullet(int bulletIndex, int spawnPositionIndex);
}