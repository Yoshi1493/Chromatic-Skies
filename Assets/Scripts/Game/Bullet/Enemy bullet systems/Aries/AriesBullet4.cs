using System.Collections;
using UnityEngine;

public class AriesBullet4 : EnemyBullet
{
    [SerializeField] float rotationSpeed;

    [SerializeField] bool rotatesClockwise;

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;

        yield return this.RotateAround(FindObjectOfType<AriesBulletSystem31>().transform.position, Mathf.Infinity, rotationSpeed, clockwise: rotatesClockwise, delay: 0.5f);
    }

    protected override void Update()
    {
        CheckCollisionWith<Player>();
        Move(MoveSpeed);
    }
}