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

        public Phase[] phases;
        public List<EnemyDiceBehaviour> enemies = new List<EnemyDiceBehaviour>();

        public void Init()
        {
            phases = RandomDiceData.phases;
            SP = 100;
            RequiredSPToSpawn = 10;
        }

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

                // don't enable next phase when
                // player haven't killed all enemies
                // player already reached the last phase
                if (phaseKillCount >= CurrentPhase.spawnAmount && CurrentPhase.phaseNumber < phases.Length)
                {
                    controller.EnableNextPhaseButton();
                }
            }
        }
    }
}