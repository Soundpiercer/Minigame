using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterDiceBehaviour : MonoBehaviour
{
    public int power;
    public float attackInterval = 0.5f;

    private void Start()
    {
        Material material = GetComponent<Renderer>().materials[0];
        material.color = new Color(0, 0, 1, 0);

        StartCoroutine(AttackEnumerator());
    }

    private IEnumerator AttackEnumerator()
    {
        while (true)
        {
            EnemyDiceBehaviour frontTarget = FindFrontEnemy(RandomDiceManager.Instance.enemies);
            if (frontTarget != null)
            {
                frontTarget.ReduceHP(power);
            }

            yield return new WaitForSeconds(attackInterval);
        }
    }

    private EnemyDiceBehaviour FindFrontEnemy(List<EnemyDiceBehaviour> enemies)
    {
        if (enemies.Count == 0) return null;
        return enemies.OrderByDescending(e => e.offset).First();
    }
}