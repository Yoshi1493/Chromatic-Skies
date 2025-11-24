using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusCommonMovement6 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        float d = SignX;
        Vector3 p0 = parentShip.transform.position;
        Vector3 p1 = new Vector2(p0.x + (-d * 6f), 2f) + (0.5f * Random.insideUnitCircle);
        Vector3 p2 = new Vector2(p1.x + Random.Range(-3f, 3f), -6f);

        yield return parentShip.SpiralIntoPoint(p1, -d * 30f, 1f);
        yield return WaitForSeconds(2f);
        yield return parentShip.SpiralIntoPoint(p2, -d * 30f, 2f);

        yield return LeaveScene();
    }
}