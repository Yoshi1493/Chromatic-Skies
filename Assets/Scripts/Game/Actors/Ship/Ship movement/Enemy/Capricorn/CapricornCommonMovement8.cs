using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornCommonMovement8 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        float d = SignX;

        yield return parentShip.TranslateAround(parentShip.transform.position + (3f * Vector3.down), d * 90f, 2f);
        yield return WaitForSeconds(1.5f);

        yield return parentShip.TranslateAround(parentShip.transform.position + (d * 3f * Vector3.left), d * 90f, 1f);
        yield return LeaveScene();
    }
}