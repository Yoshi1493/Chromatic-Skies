using System.Collections;
using UnityEngine;

public abstract class EnemyBullet : Bullet
{
    protected override void OnEnable()
    {
        base.OnEnable();

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
        transform.name += transform.GetSiblingIndex();
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