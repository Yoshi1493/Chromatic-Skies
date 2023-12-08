using UnityEngine;
using static MathHelper;

public class TaurusLaser50 : Laser
{
    static int rotationDirection;
    protected override float MaxLifetime => 10f;

    protected override void OnEnable()
    {
        base.OnEnable();
        rotationDirection = PositiveOrNegativeOne;
    }

    protected override void Update()
    {
        base.Update();

        if (active)
        {
            transform.Rotate(8f * rotationDirection * Time.deltaTime * Vector3.forward, Space.Self);
        }
    }
}