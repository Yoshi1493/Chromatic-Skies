using UnityEngine;

public abstract class Collectible : Actor
{
    int CollisionMask => 1 << LayerMask.NameToLayer("Player");
    Collider2D[] collisionResults = new Collider2D[1];
    float hitboxSize;

    Player player;
    bool foundPlayer;

    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<Player>();
    }

    protected virtual void OnEnable()
    {
        moveDirection = Vector3.down;
        MoveSpeed = 2f;

        foundPlayer = false;
    }

    void Start()
    {
        hitboxSize = Mathf.Min(SpriteRenderer.size.x, SpriteRenderer.size.y) / 2f;
    }

    void Update()
    {
        transform.Translate(Time.deltaTime * MoveSpeed * moveDirection.normalized, Space.World);

        if (!foundPlayer)
        {
            CheckCollisionWithPlayer();
        }
        else
        {
            HomeInOnPlayer();
        }
    }

    void CheckCollisionWithPlayer()
    {
        var collisions = Physics2D.OverlapCircleNonAlloc(transform.position, hitboxSize, collisionResults, CollisionMask);

        if (collisions > 0)
        {
            foundPlayer = true;
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