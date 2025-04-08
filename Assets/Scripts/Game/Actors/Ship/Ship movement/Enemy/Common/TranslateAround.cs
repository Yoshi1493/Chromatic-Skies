using System.Collections;
using UnityEngine;

public class TranslateAround : EnemyMovement
{
    [SerializeField] float rotationAmount;
    [SerializeField] Vector2 rotationPoint;

    protected override IEnumerator Move()
    {
        yield return this.TranslateAround(rotationPoint, rotationAmount, duration, delay);
    }
}