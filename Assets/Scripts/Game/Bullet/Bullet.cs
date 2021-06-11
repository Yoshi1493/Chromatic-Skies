using UnityEngine;

public class Bullet : Actor
{
    const float Lifespan = 3f;

    [SerializeField] protected float moveSpeed;

    protected override void Awake()
    {
        base.Awake();

        moveDirection = transform.up;
        Destroy(gameObject, Lifespan);          //temp; to-do: impl. object pooling
    }

    void Update()
    {
        Move(moveSpeed);
    }
}