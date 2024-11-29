using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialRed : SpecialBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 2f;
        yield return null;
    }
}