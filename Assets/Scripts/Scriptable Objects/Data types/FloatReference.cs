using System;
using UnityEngine;

[Serializable]
public class FloatReference 
{
    public float OriginalValue;

    [SerializeField] FloatObject currentValue;
    public float CurrentValue { get => currentValue.value; set { currentValue.value = value; } }
}