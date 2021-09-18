using UnityEngine;
using TMPro;

public class AttackNameDisplay : MonoBehaviour
{
    Animator anim;
    TextMeshProUGUI nameText;

    [SerializeField] StringObject[] moduleNames;
    [SerializeField] StringObject[] attackNames;

    Enemy enemy;

    void Awake()
    {
        anim = GetComponent<Animator>();
        nameText = GetComponent<TextMeshProUGUI>();

        enemy = FindObjectOfType<Enemy>();
        EnemyShooter[] shooters = enemy.GetComponentsInChildren<EnemyShooter>();
        print(shooters.Length);
        foreach (EnemyShooter es in shooters)
            es.AttackStartAction += OnEnemyAttackStart;
    }

    void OnEnemyAttackStart()
    {
        int currentAttackIndex = enemy.shipData.MaxLives.Value - enemy.shipData.CurrentLives.Value;

        nameText.text = $"{moduleNames[currentAttackIndex].value} Module | {attackNames[currentAttackIndex].value}";
        //anim.SetTrigger("Show");
    }
}