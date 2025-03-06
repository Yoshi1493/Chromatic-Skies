using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    [SerializeField] Enemy enemy;

    void Awake()
    {
        enemy.LoseLifeAction += OnLoseLife;
        enemy.DeathAction += OnDie;
    }

    void OnLoseLife()
    {
        //play lose-life sfx
    }

    void OnDie()
    {
        //play death sfx
    }
}