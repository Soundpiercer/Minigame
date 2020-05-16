using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterDiceBehaviour : MonoBehaviour
{
    public int power;
    public float attackInterval = 0.4f;

    public GameObject beamQuad;

    private void Start()
    {
        Material material = GetComponent<Renderer>().materials[0];
        material.color = new Color(0, 0, 1, 1);

        Material beam = beamQuad.GetComponent<Renderer>().materials[0];
        beam.color = new Color(1, 1, 0, 1);

        StartCoroutine(AttackEnumerator());
    }

    private IEnumerator AttackEnumerator()
    {
        while (true)
        {
            EnemyDiceBehaviour frontTarget = FindFrontEnemy(RandomDiceManager.Instance.enemies);

            if (frontTarget != null)
            {
                // Play Laser Beam Animation
                StartCoroutine(BeamAnimationEnumerator(transform, frontTarget.transform));

                // Reduce Enemy HP
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

    private IEnumerator BeamAnimationEnumerator(Transform shooter, Transform enemy)
    {
        // Beam Setup - Rotation
        Vector3 direction = enemy.position - shooter.position;

        float angle = Vector3.Angle(Vector3.up, direction);
        Vector3 cross = Vector3.Cross(Vector3.up, direction);

        if (cross.z < 0)
        {
            angle = -angle;
        }

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        beamQuad.transform.rotation = rotation;

        // Beam Setup - Position & Scale
        beamQuad.transform.localPosition = direction / 2f;

        float scale = direction.magnitude;
        beamQuad.transform.localScale = new Vector3(
            beamQuad.transform.localScale.x,
            scale,
            beamQuad.transform.localScale.z);

        // Beam Animation
        beamQuad.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        beamQuad.SetActive(false);
        yield break;
    }
}