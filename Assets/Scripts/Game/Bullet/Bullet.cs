using UnityEngine;

public abstract class Bullet : Actor
{
    [SerializeField] protected float moveSpeed;

    const float MaxLifetime = 3f;
    float currentLifetime;

    protected override void Awake()
    {
        base.Awake();

        moveDirection = transform.up;
    }

    void OnEnable()
    {
        currentLifetime = 0;
    }

    void Update()
    {
        Move(moveSpeed);

        currentLifetime += Time.deltaTime;
        if (currentLifetime > MaxLifetime) Destroy();        
    }

    protected abstract void Destroy();
}