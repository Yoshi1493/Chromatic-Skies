using System.Collections;
using UnityEngine;

public class LerpSpeed : EnemyMovement
{
    [SerializeField] float endSpeed;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(endSpeed, duration, delay);
    }
}