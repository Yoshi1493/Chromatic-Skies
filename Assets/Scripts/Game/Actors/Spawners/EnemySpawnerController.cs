using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] IntObject selectedBossIndex;
    [SerializeField] EnemySpawner[] enemySpawners;

    void Awake()
    {
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            enemySpawners[i].gameObject.SetActive(i == selectedBossIndex.value);
        }
    }
}