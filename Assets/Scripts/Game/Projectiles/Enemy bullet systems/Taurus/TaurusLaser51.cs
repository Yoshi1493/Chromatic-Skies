using UnityEngine;

public class TaurusLaser51 : Laser
{
    const float RotationSpeed = 10f;

    protected override float MaxLifetime => 6.5f;

    protected override void Update()
    {
        base.Update();

        if (active)
        {
            transform.Rotate(RotationSpeed * Time.deltaTime * Vector3.forward, Space.Self);
        }
    }
}