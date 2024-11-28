using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialYellow : PlayerBullet, ISpecialBullet
{
    IEnumerator ISpecialBullet.Move()
    {
        yield return null;
    }
}