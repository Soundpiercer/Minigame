using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomDice
{
    public class GameProcessController : MonoBehaviour
    {
        [Header("Stage Control/Standby Time")]
        public float standbyTime;

        [Header("Stage Control/Enemy")]
        public int spawnAmount;
        public float spawnInterval;

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
        public DiceSlot[] diceSlots = new DiceSlot[15];

        private const int NUMBER_OF_SLOTS = 15;

        public void Init()
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
                    diceSlots[3 * i + j] = new DiceSlot(new Vector3(-4 + 2 * i, 2 * j, 0), false);
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
            RandomDiceManager.Instance.enemies.Add(enemy);
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
    }
}