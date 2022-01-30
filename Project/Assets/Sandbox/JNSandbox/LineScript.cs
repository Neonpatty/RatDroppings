using UnityEngine;

public class LineScript : MonoBehaviour
{
    public LineRenderer linerenderer;
    public Transform[] points;

    public Color playerColor;
    public Color glowColor;

    void Update()
    {
        linerenderer.SetPosition(0, points[0].position);
        linerenderer.SetPosition(1, points[1].position);
    }
}