using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

namespace RandomDice
{
    public class EnemyDiceBehaviour : MonoBehaviour
    {
        public int hp;
        public int maxHP;
        public TMP_Text hpText;

        // the offset of the dice in the path. 0 to 1.
        public float offset;

        private PathFunction path;
        private float offsetPerSecond;

        private bool hasKilled;

        private const float ONE_FRAME_FACTOR = 1f / 60f;

        public void Init(PathFunction path, float offsetPerSecond)
        {
            this.path = path;
            this.offsetPerSecond = offsetPerSecond;

            maxHP = hp;
            hpText.text = hp.ToString();

            StartCoroutine(MoveEnumerator());
        }

        private IEnumerator MoveEnumerator()
        {
            while (true)
            {
                offset += ONE_FRAME_FACTOR * offsetPerSecond;
                transform.position = path.Lerp(offset);

                yield return new WaitForSeconds(ONE_FRAME_FACTOR);
            }
        }

        /// <summary>
        /// get attacked
        /// </summary>
        public void ReduceHP(int amount)
        {
            hp = Mathf.Clamp(hp - amount, 0, maxHP);
            hpText.text = hp.ToString();

            if (hp <= 0)
            {
                Kill();
            }
        }

        private void Kill()
        {
            if (hasKilled)
                return;

            hasKilled = true;

            RandomDiceManager.Instance.enemies.Remove(this);
            RandomDiceManager.Instance.SP += 10;
            RandomDiceManager.Instance.PhaseKillCount += 1;

            Destroy(gameObject);
        }
    }
}