using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBullet4 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;

        yield return WaitUntil(() => FindObjectsOfType<AriesBullet5>().Length == 16);
        StartCoroutine(this.RotateAround(FindObjectOfType<AriesBulletSystem31>().transform.position, Mathf.Infinity, 45f, clockwise: true, delay: 0.5f));
    }

    protected override void Update()
    {
        CheckCollisionWith<Player>();
        Move(MoveSpeed);
    }
}