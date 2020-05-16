using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFunction : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;

    // master paramaters are applied after the path function values are calculated.
    [Header("Master Parameters")]
    public float masterScaleX;
    public float masterScaleY;
    public float masterTranslationX;
    public float masterTranslationY;

    private const float START = 0f;
    private const float END = 1f;

    public Vector3 Lerp(float offset)
    {
        float x;
        float y;

        offset = Mathf.Clamp(offset, START, END);

        if (offset < 0.25f)
        {
            x = -1f;
            y = offset * 4f;
        }
        else if (offset < 0.75f)
        {
            x = (offset - 0.5f) * 4f;
            y = 1f;
        }
        else if (offset < END)
        {
            x = 1f;
            y = (1 - offset) * 4f;
        }
        else
        {
            x = endPoint.x;
            y = endPoint.y;
        }

        return new Vector3(
            x * masterScaleX + masterTranslationX,
            y * masterScaleY + masterTranslationY,
            0);
    }
}
