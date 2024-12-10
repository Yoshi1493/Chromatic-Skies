using System;
using UnityEngine;

[Serializable]
public class FloatReference
{
    [SerializeField] bool useConstant;

    [SerializeField] float ConstantValue;
    [SerializeField] FloatObject Variable;

    public float Value
    {
        get => useConstant ? ConstantValue : Variable.value;
        set { ConstantValue = value; }
    }
}