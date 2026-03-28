using UnityEngine;

public abstract class ShipHUDComponent<TShip> : MonoBehaviour
    where TShip : CharacterShip
{
    protected TShip ship;

    protected virtual void Awake()
    {
        ship = FindAnyObjectByType<TShip>();
    }
}