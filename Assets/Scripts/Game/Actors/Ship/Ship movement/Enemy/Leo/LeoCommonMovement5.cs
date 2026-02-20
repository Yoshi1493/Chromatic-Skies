using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static CameraBoundaries;

public class LeoCommonMovement5 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 p0 = new(ScreenHalfWidth * Random.Range(-0.75f, 0.75f), ScreenHalfHeight * 1.1f);

        parentShip.transform.position = p0;
        float d = -SignX;

        yield return parentShip.SpiralIntoPoint(ActorMovementHelper.bossSpawnPosition, d * 45f, 1f);

        while (enabled)
        {
            yield return WaitForSeconds(1f);

            Vector3 p1 = new(parentShip.transform.position.x, ScreenHalfHeight * 1.1f);

            yield return parentShip.MoveTo(p1, 0.5f * Mathf.Abs((ScreenHalfHeight * 1.1f) - parentShip.transform.position.y));

            Vector3 p2 = new(ScreenHalfWidth * 1.1f, Random.Range(2.5f, 4.5f));
            Vector3 p3 = new(-p2.x, Random.Range(2.5f, 4.5f));
            Vector3 p4 = new(p2.x, Random.Range(2.5f, 4.5f));

            yield return parentShip.MoveFromTo(p2, p3, 1.5f);
            yield return parentShip.MoveTo(p4, 1.5f);

            Vector3 p5 = new(ScreenHalfWidth * Random.Range(-0.25f, 0.25f), ScreenHalfHeight * 1.1f);
            Vector3 p6 = new(p5.x, ActorMovementHelper.bossSpawnPosition.y);

            yield return parentShip.MoveFromTo(p5, p6, 0.5f * Mathf.Abs(p6.y - p5.y));

            for (int i = 0; i < 3; i++)
            {
                yield return parentShip.MoveToRandomPosition(1f);
            }
        }
    }
}