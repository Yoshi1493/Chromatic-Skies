using UnityEngine;
using static MathHelper;

public class TaurusLaser50 : Laser
{
    static int rotationDirection;
    const float RotationSpeed = 10f;
    protected override float MaxLifetime => 7.5f;

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
            transform.Rotate(RotationSpeed * rotationDirection * Time.deltaTime * Vector3.forward, Space.Self);
        }
    }
}