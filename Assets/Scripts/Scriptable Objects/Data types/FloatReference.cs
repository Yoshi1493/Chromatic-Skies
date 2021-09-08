using System;
using UnityEngine;

[Serializable]
public class FloatReference
{
    [SerializeField] bool useConstant;

    [SerializeField] int ConstantValue;
    [SerializeField] FloatObject Variable;

    public float Value
    {
        get => useConstant ? ConstantValue : Variable.value;
        set { Variable.value = value; }
    }
}