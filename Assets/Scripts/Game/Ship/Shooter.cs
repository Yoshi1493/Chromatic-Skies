using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    protected ShipObject shipData;
    protected List<Transform> spawnPositions = new List<Transform>();

    protected virtual void Awake()
    {
        shipData = transform.parent.GetComponent<Ship>().shipData;

        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPositions.Add(transform.GetChild(i));
        }
    }

    protected virtual float ShootingSpeed => shipData.ShootingSpeed.CurrentValue;
    protected float ShootingCooldown => ShootingSpeed == 0 ? 0 : 1 / ShootingSpeed;

    protected abstract void SpawnBullet(int bulletIndex, int spawnPositionIndex);
}