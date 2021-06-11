using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    [SerializeField] protected ShipObject shipData;    
    new protected Transform transform;
    protected Vector2 velocity;

    public event System.Action DeathAction;

    protected virtual void Awake()
    {
        transform = GetComponent<Transform>();
        InitShipStats();

        DeathAction += Die;
    }

    void InitShipStats()
    {
        shipData.currentHealth.value = shipData.maxHealth;
        shipData.currentPower.value = shipData.originalPower;
        shipData.currentDefense.value = shipData.originalDefense;
        shipData.currentMovementSpeed.value = shipData.originalMovementSpeed;
        shipData.currentShootingSpeed.value = shipData.originalShootingSpeed;

        name = shipData.shipName.value;     //debug
    }

    protected void Move(Vector3 direction)
    {
        direction.Normalize();
        transform.position += shipData.currentMovementSpeed.value * Time.deltaTime * direction;
    }

    protected abstract void SpawnBullet(GameObject bullet);

    protected void TakeDamage(int amount)
    {
        shipData.currentHealth.value -= amount;

        if (shipData.currentHealth.value <= 0)
        {
            DeathAction?.Invoke();
        }
    }

    protected abstract void Die();
}