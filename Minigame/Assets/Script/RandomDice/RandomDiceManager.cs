using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDiceManager : MonoBehaviour
{
    [Header("Stage Control")]
    public int spawnAmount;
    public float spawnInterval;
    public float standbyTime;

    [Header("Enemy")]
    public GameObject enemyDicePrefab;
    public Transform enemyDiceRoot;

    [Header("Path Function")]
    public PathFunction path;
    public float offsetPerSecond;

    private void Start()
    {
        StartCoroutine(GameProcessEnumerator());
    }

    private IEnumerator GameProcessEnumerator()
    {
        // standby
        yield return new WaitForSeconds(standbyTime);

        // game start
        int count = 0;

        while (count < spawnAmount)
        {
            CreateEnemy();
            count++;

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void CreateEnemy()
    {
        EnemyDiceBehaviour enemy = Instantiate(enemyDicePrefab, path.startPoint, Quaternion.identity, enemyDiceRoot).GetComponent<EnemyDiceBehaviour>();
        enemy.Init(path, offsetPerSecond);
    }
}
