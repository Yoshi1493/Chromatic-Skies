using UnityEngine;

public class TaurusLaser51 : Laser
{
    Transform originalParent;

    protected override float MaxLifetime => 10f;

    protected override void Awake()
    {
        base.Awake();
        originalParent = FindObjectOfType<EnemyLaserPool>().transform;
    }

    public override void Destroy()
    {
        transform.parent = originalParent;
        base.Destroy();
    }
}