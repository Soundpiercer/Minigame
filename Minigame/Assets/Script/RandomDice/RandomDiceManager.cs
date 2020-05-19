using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomDiceManager : MonoBehaviour
{
    #region Simple Singleton
    public static RandomDiceManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }
    #endregion

    [Header("Stage Control/Enemy")]
    public int spawnAmount;
    public float spawnInterval;
    public float standbyTime;

    [Header("Stage Control/Shooter")]
    private int shooterCount;

    [Header("Shooter")]
    public GameObject shooterDicePrefab;
    public Transform shooterDiceRoot;

    [Header("Enemy")]
    public GameObject enemyDicePrefab;
    public Transform enemyDiceRoot;

    [Header("Path Function")]
    public PathFunction path;
    public float offsetPerSecond;

    [HideInInspector]
    public DiceSlot[] diceSlots = new DiceSlot[NUMBER_OF_SLOTS];

    [HideInInspector]
    public List<EnemyDiceBehaviour> enemies = new List<EnemyDiceBehaviour>();

    private const int NUMBER_OF_SLOTS = 15;

    private void Start()
    {
        InitDiceSlots();
        StartCoroutine(GameProcessEnumerator());
    }

    private void InitDiceSlots()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                diceSlots[3 * i + j].position = new Vector3(-6 + 2 * i, -6 + 2 * j, 0);
                diceSlots[3 * i + j].isOccupied = false;
            }
        }
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

    private void CreateEnemy()
    {
        EnemyDiceBehaviour enemy = Instantiate(enemyDicePrefab, path.startPoint, Quaternion.identity, enemyDiceRoot).GetComponent<EnemyDiceBehaviour>();
        enemy.Init(path, offsetPerSecond);
        enemies.Add(enemy);
    }

    public void AddDiceAtRandomSlot()
    {
        if (shooterCount >= NUMBER_OF_SLOTS)
            return;

        // Find Random Unoccupied Slot
        DiceSlot targetSlot = new DiceSlot();

        while (true)
        {
            int index = Random.Range(0, NUMBER_OF_SLOTS);
            if (diceSlots[index].isOccupied == false)
            {
                diceSlots[index].isOccupied = true;
                targetSlot = diceSlots[index];
                break;
            }
        }

        // Instantiate Shooter Dice
        Instantiate(shooterDicePrefab, targetSlot.position, Quaternion.identity, shooterDiceRoot);
        shooterCount++;
    }

    public void Exit()
    {
        SceneManager.LoadScene("Title");
    }
}
