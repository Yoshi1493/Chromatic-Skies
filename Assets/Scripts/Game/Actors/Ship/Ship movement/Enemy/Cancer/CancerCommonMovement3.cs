using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerCommonMovement3 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return LeaveScene();
    }
}