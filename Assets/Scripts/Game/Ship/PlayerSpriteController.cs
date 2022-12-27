using UnityEngine;

public class PlayerSpriteController : ShipSpriteController<Player>
{
    [SerializeField] SpriteRenderer hitboxVisualizer;

    protected override void Awake()
    {
        base.Awake();
        GetComponent<PlayerMovement>().MovementSlowAction += OnMovementSlow;
    }

    void OnMovementSlow(bool state)
    {
        hitboxVisualizer.enabled = state;
    }
}