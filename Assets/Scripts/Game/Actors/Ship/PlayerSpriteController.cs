using UnityEngine;

public class PlayerSpriteController : ShipSpriteController<Player>
{
    [SerializeField] SpriteRenderer hitboxVisualizer;

    PlayerMovement playerMovement;

    protected override void Awake()
    {
        base.Awake();

        playerMovement = GetComponent<PlayerMovement>();
    }

    protected override void Start()
    {
        base.Start();
        playerMovement.MovementSlowAction += OnMovementSlow;
    }

    void OnMovementSlow(bool state)
    {
        if (!PauseHandler.IsPaused)
        {
            hitboxVisualizer.enabled = state;
        }
    }
}