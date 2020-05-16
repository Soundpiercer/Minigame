using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class EnemyDiceBehaviour : MonoBehaviour
{
    public int hp;
    public TMP_Text hpText;

    private float offset;

    private PathFunction path;
    private float offsetPerSecond;

    private const float ONE_FRAME_FACTOR = 1f / 60f;

    public void Init(PathFunction path, float offsetPerSecond)
    {
        this.path = path;
        this.offsetPerSecond = offsetPerSecond;

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
}