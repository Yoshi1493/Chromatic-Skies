using UnityEngine;

public class PlayerSpriteController : ShipSpriteController<Player>
{
    [SerializeField] SpriteRenderer hitboxVisualizer;

    PauseHandler pauseHandler;

    protected override void Awake()
    {
        base.Awake();

        GetComponent<PlayerMovement>().MovementSlowAction += OnMovementSlow;
        pauseHandler = FindObjectOfType<PauseHandler>();
    }

    void OnMovementSlow(bool state)
    {
        if (!pauseHandler.IsPaused)
        {
            hitboxVisualizer.enabled = state;
        }
    }
}