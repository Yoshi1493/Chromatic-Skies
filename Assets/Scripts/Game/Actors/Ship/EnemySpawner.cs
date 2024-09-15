using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] IntObject selectedEnemyIndex;
    [SerializeField] GameObject[] enemyPrefabs;

    void OnEnable()
    {
        Instantiate(enemyPrefabs[selectedEnemyIndex.value], transform.position, transform.rotation);
    }
}