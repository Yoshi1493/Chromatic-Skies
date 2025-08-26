using UnityEngine;

public abstract class Collectible : Actor
{
    const float BounceDuration = 1.5f;
    float currentLifetime;
    protected virtual float MaxLifetime => 10f;

    protected Player player;
    const float PlayerDetectionSqRadius = 1f;
    const float PlayerCollisionSqRadius = 0.1f;

    [HideInInspector] public bool foundPlayer;

    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<Player>();
    }

    protected virtual void OnEnable()
    {
        moveDirection = Vector3.up;
        MoveSpeed = 2f;

        currentLifetime = 0f;

        foundPlayer = false;
    }

    void Update()
    {
        CheckPosition();
        IncrementLifetime();
        Move();
        CheckPlayerPosition();
    }

    void IncrementLifetime()
    {
        currentLifetime += Time.deltaTime;

        if (currentLifetime > MaxLifetime)
        {
            Destroy();
        }
    }

    void Move()
    {
        //set speed and direction based on whether or not Player is detected
        if (!foundPlayer)
        {
            if (currentLifetime < BounceDuration)
            {
                moveDirection = Vector3.Lerp(Vector3.up, Vector3.down, currentLifetime / BounceDuration);
            }
        }
        else
        {
            moveDirection = (player.transform.position - transform.position).normalized;
            MoveSpeed = 15f;
        }

        //apply movement
        transform.Translate(Time.deltaTime * MoveSpeed * moveDirection, Space.World);
    }

    //destroy if outside camera left/right/bottom bounds
    void CheckPosition()
    {
        if (!this.IsWithinCameraBounds() && transform.position.y < CameraBoundaries.ScreenHalfHeight)
        {
            Destroy();
        }
    }

    void CheckPlayerPosition()
    {
        Vector3 diff = player.transform.position - transform.position;

        //check if gameobject is near Player
        if (!foundPlayer)
        {
            //check if Player is in PoC range
            if (player.transform.position.y >= CameraBoundaries.ScreenHalfHeight * 0.5f)
            {
                foundPlayer = true;
            }
            else
            {
                if (Vector3.SqrMagnitude(diff) <= PlayerDetectionSqRadius)
                {
                    foundPlayer = true;
                }
            }
        }

        //apply collectible effect + destroy if touching Player
        if (Vector3.SqrMagnitude(diff) <= PlayerCollisionSqRadius)
        {
            Collect();
            Destroy();
        }
    }

    protected abstract void Collect();

    protected virtual void Destroy()
    {
        moveDirection = Vector3.zero;
        MoveSpeed = 0f;
    }
}