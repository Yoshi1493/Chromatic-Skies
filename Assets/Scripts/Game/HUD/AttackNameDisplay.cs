using System.Linq;
using UnityEngine;
using TMPro;

public class AttackNameDisplay : MonoBehaviour
{
    Enemy enemy;

    Animator anim;
    TextMeshProUGUI nameText;

    [SerializeField] StringObject[] moduleNames;
    [SerializeField] StringObject[] attackNames;

    void Awake()
    {
        anim = GetComponent<Animator>();
        nameText = GetComponent<TextMeshProUGUI>();

        enemy = FindObjectOfType<Enemy>();
        enemy.LoseLifeAction += OnEnemyLoseLife;

        var shooters = enemy.GetComponentsInChildren<EnemyBulletSystem>().Where(i => !(i is EnemyBulletSubsystem));

        foreach (EnemyShooter es in shooters)
            es.AttackStartAction += OnEnemyAttackStart;
    }

    void OnEnemyAttackStart()
    {
        int currentAttackIndex = enemy.shipData.MaxLives.Value - enemy.shipData.CurrentLives.Value;
        nameText.text = $"{moduleNames[currentAttackIndex].value} Module | {attackNames[currentAttackIndex].value}";

        anim.SetBool("show_name", true);
    }

    void OnEnemyLoseLife()
    {
        anim.SetBool("show_name", false);
    }
}