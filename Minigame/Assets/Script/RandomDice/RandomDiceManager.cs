using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RandomDice
{
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

        public RandomDiceController controller;
        public List<EnemyDiceBehaviour> enemies = new List<EnemyDiceBehaviour>();

        [HideInInspector]
        public Phase[] phases = RandomDiceData.phases;

        private int sp;
        public int SP
        {
            get { return sp; }
            set
            {
                sp = value;
                controller.ShowSP(sp);
            }
        }
        private int requiredSPToSpawn;
        public int RequiredSPToSpawn
        {
            get { return requiredSPToSpawn; }
            set
            {
                requiredSPToSpawn = value;
                controller.ShowRequiredSPToSpawn(requiredSPToSpawn);
            }
        }
        private Phase currentPhase;
        public Phase CurrentPhase
        {
            get { return currentPhase; }
            set
            {
                currentPhase = value;
                controller.ShowPhaseNumber(currentPhase.phaseNumber);
            }
        }
        private int phaseKillCount;
        public int PhaseKillCount
        {
            get { return phaseKillCount; }
            set
            {
                phaseKillCount = value;
                if (phaseKillCount >= CurrentPhase.spawnAmount)
                {
                    controller.EnableNextPhaseButton();
                }
            }
        }
    }
}