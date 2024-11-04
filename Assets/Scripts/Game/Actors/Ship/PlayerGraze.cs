using System;
using UnityEngine;

public class PlayerGraze : MonoBehaviour
{
    public event Action GrazeAction;

    [SerializeField] IntObject playerGraze;

    void Awake()
    {
        
    }
}