using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override void OnEnable()
    {
        base.OnEnable();
        moveDirection = transform.up;
    }

    protected override void Update()
    {
        base.Update();
        CheckCollisionWith<Enemy>();
    }

    public override void Destroy()
    {
        if (movementBehaviour != null) StopCoroutine(movementBehaviour);
        PlayerBulletPool.Instance.ReturnToPool(this);
    }
}