using System;
using UnityEngine;

public class PlayerGraze : MonoBehaviour
{
    public event Action GrazeAction;

    [SerializeField] IntObject playerGraze;

    void Start()
    {
        playerGraze.value = 0;
    }

    public void GrazePlayer()
    {
        playerGraze.value++;
        GrazeAction?.Invoke();
    }
}