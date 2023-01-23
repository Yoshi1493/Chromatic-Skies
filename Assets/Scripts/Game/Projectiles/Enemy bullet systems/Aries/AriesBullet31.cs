using System.Collections;
using UnityEngine;

public class AriesBullet31 : EnemyBullet
{
    [SerializeField] float rotationSpeed;
    [SerializeField] bool rotatesClockwise;

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return this.RotateAround(2f * Vector3.down, MaxLifetime, rotationSpeed, clockwise: rotatesClockwise);
    }
}