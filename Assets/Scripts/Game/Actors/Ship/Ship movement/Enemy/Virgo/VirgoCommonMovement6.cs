using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoCommonMovement6 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return LeaveScene();
    }
}