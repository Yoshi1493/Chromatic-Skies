using UnityEngine;

public abstract class Collectible : Actor
{
    const float BounceDuration = 1.5f;
    float currentLifetime;
    protected virtual float MaxLifetime => 10f;

    Player player;
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

        transform.Translate(Time.deltaTime * MoveSpeed * moveDirection, Space.World);
    }

    void CheckPlayerPosition()
    {
        Vector3 diff = player.transform.position - transform.position;

        if (!foundPlayer)
        {
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

        if (Vector3.SqrMagnitude(diff) <= PlayerCollisionSqRadius)
        {
            Destroy();
        }
    }

    public virtual void Destroy()
    {
        moveDirection = Vector3.zero;
        MoveSpeed = 0f;
    }
}