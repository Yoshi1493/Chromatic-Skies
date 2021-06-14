using System;
using UnityEngine;

[Serializable]
public class IntReference
{
    public int OriginalValue;

    [SerializeField] IntObject currentValue;
    public int CurrentValue { get => currentValue.value; set { currentValue.value = value; } }
}