using UnityEngine;

public abstract class Collectible : Actor
{
    const float BounceLerpDuration = 1.5f;
    float currentLifetime;

    Player player;
    const float PlayerDetectionSqRadius = 1f;
    const float PlayerCollisionSqRadius = 0.1f;
    bool foundPlayer;

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
        currentLifetime += Time.deltaTime;

        Move();
        CheckCollisionWithPlayer();
    }

    void Move()
    {
        if (currentLifetime < BounceLerpDuration && !foundPlayer)
        {
            moveDirection = Vector3.Lerp(Vector3.up, Vector3.down, currentLifetime / BounceLerpDuration);
        }

        transform.Translate(Time.deltaTime * MoveSpeed * moveDirection, Space.World);
    }

    void CheckCollisionWithPlayer()
    {
        Vector3 diff = player.transform.position - transform.position;
        
        if (Vector3.SqrMagnitude(diff) <= PlayerDetectionSqRadius)
        {
            foundPlayer = true;

            moveDirection = diff;
            MoveSpeed = 15f;

            if (Vector3.SqrMagnitude(diff) <= PlayerCollisionSqRadius)
            {
                Destroy();
            }
        }
    }

    public virtual void Destroy()
    {
        moveDirection = Vector3.zero;
        MoveSpeed = 0f;
    }
}