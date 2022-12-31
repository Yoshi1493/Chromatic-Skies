using System.Collections;
using UnityEngine;

public class AriesBullet31 : EnemyBullet
{
    [SerializeField] float rotationSpeed;
    [SerializeField] bool rotatesClockwise;

    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return this.RotateAround(FindObjectOfType<AriesBulletSystem31>().transform.position, MaxLifetime, rotationSpeed, clockwise: rotatesClockwise);
    }
}