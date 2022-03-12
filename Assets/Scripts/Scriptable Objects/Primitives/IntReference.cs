using System;
using UnityEngine;

[Serializable]
public class IntReference
{
    [SerializeField] bool useConstant;

    [SerializeField] int ConstantValue;
    [SerializeField] IntObject Variable;

    public int Value
    {
        get => useConstant ? ConstantValue : Variable.value;
        set { Variable.value = value; }
    }
}