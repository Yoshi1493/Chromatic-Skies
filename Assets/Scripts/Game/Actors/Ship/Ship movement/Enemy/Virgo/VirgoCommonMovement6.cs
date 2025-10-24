using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoCommonMovement6 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 endPosition = transform.position + new Vector3(-2f, -4f);

        yield return parentShip.SpiralIntoPoint(endPosition, SignX * 180f, 1.5f);
        yield return WaitForSeconds(6f);

        yield return LeaveScene();
    }
}