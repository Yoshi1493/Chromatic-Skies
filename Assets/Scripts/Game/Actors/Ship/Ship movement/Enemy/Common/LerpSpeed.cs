using System.Collections;
using UnityEngine;

public class LerpSpeed : EnemyMovement
{
    [SerializeField] float delay;
    [SerializeField] float endSpeed;
    [SerializeField] float duration;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(endSpeed, duration, delay);
    }
}