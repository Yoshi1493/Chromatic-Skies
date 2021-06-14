using UnityEngine;
using System.Collections.Generic;

public abstract class Shooter : MonoBehaviour
{
    protected virtual float ShootingSpeed => transform.parent.GetComponent<Ship>().shipData.ShootingSpeed.CurrentValue;
    protected float ShootingCooldown => 1 / ShootingSpeed;

    protected abstract void Update();

    protected abstract void SpawnBullet();

    protected List<Transform> spawnPositions = new List<Transform>();

    protected void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPositions.Add(transform.GetChild(i));
        }
    }
}