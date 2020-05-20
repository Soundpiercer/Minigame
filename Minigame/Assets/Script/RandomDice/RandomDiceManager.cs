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
        [HideInInspector]
        public List<EnemyDiceBehaviour> enemies = new List<EnemyDiceBehaviour>();
        [HideInInspector]
        public Phase[] phases =
        {
            new Phase(1, 10, 1.0f, 0.075f),
            new Phase(2, 15, 0.9f, 0.08f),
            new Phase(3, 20, 0.8f, 0.085f),
            new Phase(4, 25, 0.7f, 0.09f),
            new Phase(5, 30, 0.6f, 0.095f)
        };

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
    }
}