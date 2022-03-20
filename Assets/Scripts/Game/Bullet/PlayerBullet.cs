using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override int CollisionMask => 1 << LayerMask.NameToLayer("Enemy");

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

    protected override void HandleCollisionWithShip<TShip>(Collider2D coll)
    {
        //get particle spawn position+rotation
        Vector3 pos = transform.position;
        float rot = coll.transform.position.GetRotationDifference(transform.position);
        SpawnDestructionParticles(pos, rot);

        base.HandleCollisionWithShip<TShip>(coll);
    }

    public override void Destroy()
    {
        PlayerBulletPool.Instance.ReturnToPool(this);
    }
}