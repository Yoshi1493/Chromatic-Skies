using System.Collections;
using UnityEngine;

public class AquariusBullet30 : EnemyBullet
{
    [SerializeField] bool rotatesClockwise;

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        StartCoroutine(this.LerpSpeed(0f, -3f, 3f));
        yield return this.RotateBy(120f, 3f, rotatesClockwise);
    }
}