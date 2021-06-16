using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    protected ShipObject ShipData => transform.parent.GetComponent<Ship>().shipData;

    protected virtual float ShootingSpeed => ShipData.ShootingSpeed.CurrentValue;
    protected float ShootingCooldown => ShootingSpeed == 0 ? 0 : 1 / ShootingSpeed;

    protected abstract void SpawnBullet(int index);
}