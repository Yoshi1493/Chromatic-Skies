using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialYellow : SpecialBullet
{
    protected override IEnumerator Move()
    {
        yield return null;
    }
}