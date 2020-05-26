using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomDice
{
    public class ShooterDiceBehaviour : MonoBehaviour
    {
        public ShooterDiceProperty property;

        public GameObject beamQuad;

        public void Init(ShooterDiceProperty property)
        {
            this.property = property;

            SetMaterialColor();
            StartCoroutine(AttackEnumerator());
        }

        private void SetMaterialColor()
        {
            Material material = GetComponent<Renderer>().materials[0];
            material.color = property.bodyColor;

            Material beam = beamQuad.GetComponent<Renderer>().materials[0];
            beam.color = property.beamColor;
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
                    frontTarget.ReduceHP(property.power);
                }

                yield return new WaitForSeconds(property.attackInterval);
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
}