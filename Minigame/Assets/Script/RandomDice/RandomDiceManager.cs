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

        [HideInInspector]
        public List<EnemyDiceBehaviour> enemies = new List<EnemyDiceBehaviour>();
    }
}