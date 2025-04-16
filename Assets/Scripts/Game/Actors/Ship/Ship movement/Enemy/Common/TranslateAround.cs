using System.Collections;
using UnityEngine;

public class TranslateAround : CommonEnemyMovement
{
    [SerializeField] float rotationAmount;
    [SerializeField] Vector2 rotationPoint;

    protected override IEnumerator Move()
    {
        yield return parentShip.TranslateAround(rotationPoint, rotationAmount, duration, delay);
    }
}