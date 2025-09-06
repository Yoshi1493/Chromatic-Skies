using System.Collections;
using UnityEngine;

public class PiscesCommonMovement2 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return LeaveScene();
    }
}