using UnityEngine;

public abstract class Collectible : Actor
{
    int CollisionMask => 1 << LayerMask.NameToLayer("Player");
    Collider2D[] collisionResults = new Collider2D[1];
    float hitboxSize;

    const float BounceLerpDuration = 1.5f;
    float currentLifetime;

    Player player;
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

    void Start()
    {
        hitboxSize = Mathf.Min(SpriteRenderer.size.x, SpriteRenderer.size.y) / 2f;
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
        if (!foundPlayer)
        {
            var collisions = Physics2D.OverlapCircleNonAlloc(transform.position, hitboxSize, collisionResults, CollisionMask);

            if (collisions > 0)
            {
                foundPlayer = true;
            }
        }
        else
        {
            HomeInOnPlayer();
        }
    }

    void HomeInOnPlayer()
    {
        Vector3 diff = player.transform.position - transform.position;
        moveDirection = diff;
        MoveSpeed = 10f;

        if (Vector3.SqrMagnitude(diff) <= 0.1f)
        {
            Destroy();
        }
    }

    public virtual void Destroy()
    {
        moveDirection = Vector3.zero;
        MoveSpeed = 0f;
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        if (UnityEditor.EditorApplication.isPlaying)
            Gizmos.DrawSphere(transform.position, hitboxSize);
    }
#endif
}