using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialGreen : SpecialBullet
{
    protected override IEnumerator Move()
    {
        yield return null;
    }
}