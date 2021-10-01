using System.Collections;
using UnityEngine;

public class AriesBullet4 : EnemyBullet
{
    [SerializeField] float rotationSpeed;

    [Tooltip("true: clockwise; false: anticlockwise")]
    [SerializeField] bool rotationDirection;

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;

        yield return this.RotateAround(FindObjectOfType<AriesBulletSystem31>().transform.position, Mathf.Infinity, rotationSpeed, clockwise: rotationDirection, delay: 0.5f);
    }

    protected override void Update()
    {
        CheckCollisionWith<Player>();
        Move(MoveSpeed);
    }
}