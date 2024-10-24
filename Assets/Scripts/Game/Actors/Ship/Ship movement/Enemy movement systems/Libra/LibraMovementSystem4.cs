using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraMovementSystem4 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        float w = screenHalfWidth * 2 * 1.2f;
        Vector3 p0 = parentShip.transform.position;
        Vector3 p1 = new(w * 0.5f * Mathf.Sign(p0.x), Random.Range(1f, screenHalfHeight - 1f));
        float s1 = (p1 - p0).magnitude / shipData.MovementSpeed.Value;

        Vector3 p2 = new(-p1.x, p1.y);
        float mag = (p0 - p2).magnitude;
        float dx = Mathf.Abs(p0.x - p2.x);
        float acos = Mathf.Acos(dx / mag) * Mathf.Sign(p0.y - p1.y);
        float dy = Mathf.Sin(acos) * mag;
        p2.y = p0.y + dy;

        float s2 = (p0 - p2).magnitude / shipData.MovementSpeed.Value;

        yield return this.MoveTo(p1, s1);
        yield return this.MoveFromTo(p2, p0, s2);

        yield return WaitForSeconds(3f);

        yield return this.MoveToRandomPosition(2f, 2f, 5f);
    }
}