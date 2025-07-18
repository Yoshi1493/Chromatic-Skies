using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static CameraBoundaries;

public class ScorpioBossMovement2 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToRandomPosition(2f, 3f, 4f);

        yield return WaitForSeconds(2.5f);

        Vector3 p0 = parentShip.transform.position;
        yield return parentShip.MoveToRandomPosition(1f, 2f, 2f);

        Vector3 p1 = (1.1f * (p0 - transform.position)) + transform.position;
        yield return parentShip.MoveTo(p1, 1f);

        Vector3 p2 = new(Mathf.Sign(-p1.x) * ScreenHalfWidth * 1.1f, 0.9f * Random.Range(0f, ScreenHalfHeight));
        yield return parentShip.MoveTo(p2, 1.5f);

        Vector3 p3 = new(0f, ScreenHalfHeight * 1.1f);
        Vector3 p4 = new(p3.x, 2.5f);
        yield return parentShip.MoveFromTo(p3, p4, 2f);
    }
}