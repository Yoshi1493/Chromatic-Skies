using System.Threading.Tasks;
using UnityEngine;

public class Player : Ship
{
    [SerializeField] SpriteRenderer hitboxVisualizer;

    protected override void Awake()
    {
        base.Awake();
        FindObjectOfType<PauseHandler>().GamePauseAction += OnGamePaused;
    }

    protected override void Update()
    {
        base.Update();

        GetMovementInput();
        GetSlowInput();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.L))
            TakeDamage(currentHealth);
#endif
    }

    void GetMovementInput()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
    }

    protected override void Move(Vector3 direction, float speed)
    {
        //check for collision on world boundaries along x and y axes independently
        RaycastHit2D rayH = Physics2D.Raycast(transform.position, Vector3.right, 0.1f * moveDirection.x, shipData.boundaryLayer);
        RaycastHit2D rayV = Physics2D.Raycast(transform.position, Vector3.up, 0.1f * moveDirection.y, shipData.boundaryLayer);

        //if movement is restricted on one axis, still allow movement on the other axis
        if (rayH.collider != null)
            moveDirection.x = 0;

        if (rayV.collider != null)
            moveDirection.y = 0;

        base.Move(moveDirection.normalized, speed);
    }

    void GetSlowInput()
    {
        if (Input.GetButtonDown("Slow"))
        {
            SetSlowState(true);
        }

        if (Input.GetButtonUp("Slow"))
        {
            SetSlowState(false);
        }
    }

    void SetSlowState(bool state)
    {
        hitboxVisualizer.enabled = state;
        currentSpeed = shipData.MovementSpeed.Value * (state ? 0.5f : 1);
    }

    //to-do: improve scalability?
    protected override async void LoseLife()
    {
        base.LoseLife();

        if (currentLives > 0)
        {
            await Task.Delay(RespawnTime);

            invincible = false;
            SetSpriteAlpha(1f);
        }
    }

    protected override void Die()
    {
        spriteRenderer.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        enabled = false;
    }

    void OnGamePaused(bool state)
    {
        enabled = !state;

        if (state)
            SetSlowState(false);
    }
}