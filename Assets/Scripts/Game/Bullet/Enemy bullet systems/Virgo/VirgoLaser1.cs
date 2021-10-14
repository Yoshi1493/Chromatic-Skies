using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirgoLaser1 : Laser
{
    protected override void Update()
    {
        base.Update();
        Move(MoveSpeed);
    }
}