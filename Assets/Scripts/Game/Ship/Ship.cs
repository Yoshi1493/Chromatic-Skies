using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    [SerializeField] protected ShipObject shipData;
    
    new protected Transform transform;

    protected void Awake()
    {
        transform = GetComponent<Transform>();
        name = shipData.shipName.value;
    }

    protected abstract void Update();

    protected void Move(Vector3 deltaMovement)
    {
        deltaMovement.Normalize();
        transform.position += shipData.movementSpeed.value * Time.deltaTime * deltaMovement;
    }
}