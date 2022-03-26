using System.Collections;
using UnityEngine;

public class LibraBullet21 : EnemyBullet
{
    protected override float MaxLifetime => 6f;
    [SerializeField] bool rotatesClockwise;

    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;
        yield return this.RotateBy(180f, MaxLifetime, rotatesClockwise);
    }
}