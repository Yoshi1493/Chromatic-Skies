using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialGreen1 : SpecialBullet
{
    protected override IEnumerator Move()
    {
        yield return null;
    }
}