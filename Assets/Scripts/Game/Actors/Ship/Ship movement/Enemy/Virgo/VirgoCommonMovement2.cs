using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoCommonMovement2 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return LeaveScene();
    }
}