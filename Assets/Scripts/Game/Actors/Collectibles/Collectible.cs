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
        //destroy if outside camera left/right/bottom bounds
        if (!transform.position.IsWithinCameraBounds() && transform.position.y < CameraBoundaries.ScreenHalfHeight)
        {
            Destroy();
        }
        else
        {
            Move();
            CheckPlayerPosition();
            IncrementLifetime();
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

    void IncrementLifetime()
    {
        currentLifetime += Time.deltaTime;

        if (currentLifetime > MaxLifetime)
        {
            Destroy();
        }
    }

    protected abstract void Collect();

    //return object to respective pool (called in child classes)
    protected virtual void Destroy()
    {
        moveDirection = Vector3.zero;
        MoveSpeed = 0f;
    }
}