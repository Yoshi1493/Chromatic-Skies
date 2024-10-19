using UnityEngine;

public class PlayerSpriteController : ShipSpriteController<Player>
{
    [SerializeField] SpriteRenderer hitboxVisualizer;

    PlayerMovement playerMovement;
    PauseHandler pauseHandler;

    protected override void Awake()
    {
        base.Awake();

        playerMovement = GetComponent<PlayerMovement>();
        pauseHandler = FindObjectOfType<PauseHandler>();
    }

    protected override void Start()
    {
        base.Start();
        playerMovement.MovementSlowAction += OnMovementSlow;
    }

    void OnMovementSlow(bool state)
    {
        if (!pauseHandler.IsPaused)
        {
            hitboxVisualizer.enabled = state;
        }
    }
}