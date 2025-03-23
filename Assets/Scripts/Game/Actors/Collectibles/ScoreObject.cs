using System.Collections;
using UnityEngine;

public class ScoreObject : MonoBehaviour
{
    Player player;

    void Awake()
    {
        player = FindObjectOfType<Player>();
    }
}