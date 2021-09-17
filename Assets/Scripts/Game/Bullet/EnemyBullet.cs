using System.Collections;
using UnityEngine;

public abstract class EnemyBullet : Bullet
{
    protected Enemy ownerShip;
    protected Player playerShip;

    protected override void OnEnable()
    {
        base.OnEnable();

        spriteRenderer.color = projectileData.colour.Evaluate(Random.value);

        moveDirection = new Vector2
            (
                Mathf.Sin(transform.localEulerAngles.z * Mathf.Deg2Rad),
                -Mathf.Cos(transform.localEulerAngles.z * Mathf.Deg2Rad)
            );

        if (movementBehaviour != null) StopCoroutine(movementBehaviour);
        movementBehaviour = Move();
        StartCoroutine(movementBehaviour);
    }

    void Start()
    {
        ownerShip = FindObjectOfType<Enemy>();
        playerShip = FindObjectOfType<Player>();
    }

    protected override void Update()
    {
        base.Update();
        CheckCollisionWith<Player>();
    }

    public override void Destroy()
    {
        if (movementBehaviour != null) StopCoroutine(movementBehaviour);
        EnemyBulletPool.Instance.ReturnToPool(this);
    }

    protected abstract IEnumerator Move();
}