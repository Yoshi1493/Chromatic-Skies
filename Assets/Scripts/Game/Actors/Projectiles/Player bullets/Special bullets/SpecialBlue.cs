using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialBlue : PlayerBullet, ISpecialBullet
{
    IEnumerator ISpecialBullet.Move()
    {
        yield return null;
    }
}