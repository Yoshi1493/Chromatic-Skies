using System.Collections;
using UnityEngine;

public class AriesCommonMovement08 : CommonEnemyMovement
{
    [SerializeField] Vector2 rotationPoint;

    protected override IEnumerator Move()
    {
        yield return parentShip.TranslateAround(rotationPoint, 180f, Random.Range(3f, 4f));

        yield return LeaveScene();
    }
}