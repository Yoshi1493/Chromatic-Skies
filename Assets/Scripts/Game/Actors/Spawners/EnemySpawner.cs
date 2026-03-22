using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CoroutineHelper;

public class EnemySpawner : MonoBehaviour
{
    new Transform transform;

    [SerializeField] TextAsset spawnTextFile;
    [SerializeField] CommonEnemy[] enemyPrefabs;
    [SerializeField] Boss bossPrefab;

    IEnumerator enemySpawnCoroutine;
    public event Action BossSpawnAction;

    List<CommonEnemy> enemies = new();
    [SerializeField] int minibossIndex;

    void Awake()
    {
        transform = GetComponent<Transform>();

        if (enemySpawnCoroutine != null)
        {
            StopCoroutine(enemySpawnCoroutine);
        }
        enemySpawnCoroutine = SpawnEnemies();
        StartCoroutine(enemySpawnCoroutine);
    }

    IEnumerator SpawnEnemies()
    {
        //pre-spawn boss first
        var boss = Instantiate(bossPrefab, transform.position, transform.rotation);
        yield return null;

        boss.gameObject.SetActive(false);

        if (spawnTextFile != null)
        {
            //parse spawn file
            var splitFile = spawnTextFile.text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            splitFile = splitFile.Where(i => char.IsDigit(i[0])).ToArray();

            List<float> spawnTimes = new();
            List<int> enemyIndexes = new();
            List<float> xPositions = new();
            List<float> yPositions = new();

            #region DEBUG
            if (splitFile.Length > 0)
            #endregion
            {
                foreach (var line in splitFile)
                {
                    if (char.IsDigit(line[0]))
                    {
                        var splitLine = line.Split(", ", StringSplitOptions.RemoveEmptyEntries);

                        spawnTimes.Add(float.Parse(splitLine[0]));
                        enemyIndexes.Add(int.Parse(splitLine[1]));
                        xPositions.Add(float.Parse(splitLine[2]));
                        yPositions.Add(float.Parse(splitLine[3]));
                    }
                }

                if (enemyPrefabs.Length > 0)
                {
                    //pre-spawn all enemies
                    for (int i = 0; i < enemyIndexes.Count; i++)
                    {
                        //make sure enemy index is within spawn array
                        if (enemyIndexes[i] < enemyPrefabs.Length)
                        {
                            var enemy = Instantiate(enemyPrefabs[enemyIndexes[i]], transform);
                            Vector2 pos = new(xPositions[i], yPositions[i]);
                            enemy.transform.SetPositionAndRotation(pos, transform.rotation);

                            enemy.enabled = false;
                            enemy.gameObject.SetActive(false);

                            enemies.Add(enemy);
                        }
                    }
                }

                #region DEBUG

                if (spawnTimes[0] > 3.0f)
                {
                    float t = spawnTimes[0];

                    for (int i = 0; i < spawnTimes.Count; i++)
                    {
                        spawnTimes[i] = spawnTimes[i] - t + 3.0f;
                    }
                }

                #endregion

                //activate all enemies based on spawn time *relative to previous enemy's spawn time*
                for (int i = 0; i < enemies.Count; i++)
                {
                    float delay = i > 0 ? spawnTimes[i] - spawnTimes[i - 1] : spawnTimes[0];

                    if (delay > 0f)
                    {
                        yield return WaitForSeconds(delay);
                    }

                    //wait until any enemies leave scene before spawning miniboss, plus 1 sec.
                    if (enemyIndexes[i] == minibossIndex)
                    {
                        yield return WaitUntil(IsSceneEmpty);

                        yield return WaitForSeconds(1f);
                    }

                    enemies[i].gameObject.SetActive(true);
                    enemies[i].enabled = true;

                    //wait until miniboss is defeated
                    if (enemyIndexes[i] == minibossIndex)
                    {
                        yield return WaitUntil(() => enemies[i].GetComponent<Miniboss>().HealthPercent <= 0f);
                    }
                }

                yield return WaitUntil(IsSceneEmpty);
            }
        }

        yield return WaitForSeconds(5f);

        EnemyBulletPool.Instance.DestroyAllProjectilesInPool();

        //activate boss
        boss.gameObject.SetActive(true);
        BossSpawnAction?.Invoke();
    }

    bool IsSceneEmpty()
    {
        bool noEnemies;
        bool noBullets;

        if (transform.childCount == 0)
        {
            noEnemies = true;
        }
        else
        {
            noEnemies = !transform.GetChild(0).gameObject.activeSelf;
        }

        if (EnemyBulletPool.Instance.transform.childCount == 0)
        {
            noBullets = true;
        }
        else
        {
            noBullets = EnemyBulletPool.Instance.PoolCount == EnemyBulletPool.Instance.transform.childCount;
        }

        return noEnemies && noBullets;
    }
}