using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    new protected Transform transform;

    [SerializeField] protected ShipObject shipData;
    [SerializeField] protected Transform bulletSpawnPos;

    protected void Awake()
    {
        transform = GetComponent<Transform>();
        name = shipData.shipName;
    }

    protected abstract void Update();

    protected void Move(Vector3 deltaMovement)
    {
        deltaMovement.Normalize();
        transform.position += shipData.movementSpeed.value * Time.deltaTime * deltaMovement;
    }

    protected abstract void Shoot(GameObject bullet);
}