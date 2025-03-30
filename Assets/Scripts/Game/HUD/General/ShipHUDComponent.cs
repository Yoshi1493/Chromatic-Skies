using UnityEngine;

public abstract class ShipHUDComponent<TShip> : MonoBehaviour
    where TShip : Ship
{
    protected TShip ship;

    protected virtual void Awake()
    {
        ship = FindObjectOfType<TShip>();
    }
}