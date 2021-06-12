using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override void Destroy()
    {
        PlayerBulletPool.Instance.ReturnToPool(this);
    }
}