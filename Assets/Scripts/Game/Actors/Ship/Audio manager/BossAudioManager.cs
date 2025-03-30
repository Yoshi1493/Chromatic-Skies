using UnityEngine;

public class BossAudioManager : MonoBehaviour
{
    [SerializeField] Boss boss;

    void Awake()
    {
        boss.LoseLifeAction += OnLoseLife;
        boss.DeathAction += OnDie;
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