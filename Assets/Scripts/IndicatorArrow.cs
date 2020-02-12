using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using crass;

public class IndicatorArrow : MonoBehaviour
{
    public static bool ShowArrows = true;

    public Vector2 TextOffset;
    public Vector2Int CanvasDimensions;

    public Transform TextParent;

    public Text FillText, LabelText;
    public Image Arrow;

    public CanvasGroup OverallGroup;

    public Renderer targetVisual;
    string title;

    Vector2 half = new Vector2(0.5f, 0.5f);

    void Update ()
    {
        OverallGroup.alpha = ShowArrows ? 1 : 0;

        setPositions();
        setTexts();
    }

    public void Initialize (Renderer targetVisual, string title)
    {
        this.targetVisual = targetVisual;
        this.title = title;
    }

    void setPositions ()
    {
        Vector3 cameraViewportPoint = CameraCache.Main.WorldToViewportPoint(targetVisual.transform.position);
        Vector2 viewportPosition = cameraViewportPoint;
        Vector2 arrowDirection;

        // clamp to screen if outside
        if (cameraViewportPoint.z < 0 || viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            // if the object is behind us, we need to do extra rotation to see it. flipping the viewportPosition perfectly simulates that extra rotation
            if (cameraViewportPoint.z < 0) viewportPosition *= -1;

            // map from [0,1] to [-1,1] so we can compare absolute values below
            viewportPosition = (viewportPosition - half) * 2;

            float absX = Mathf.Abs(viewportPosition.x);
            float absY = Mathf.Abs(viewportPosition.y);

            arrowDirection = new Vector2
            (
                absX > absY ? Mathf.Sign(viewportPosition.x) : 0,
                absX > absY ? 0 : Mathf.Sign(viewportPosition.y)
            );
        
            float maxComponent = Mathf.Max(absX, absY);

            // undo the mapping and clamp
            viewportPosition = (viewportPosition / (maxComponent * 2)) + half;
        }
        else
        {
            arrowDirection = Vector2.down;
        }

        Arrow.transform.position = Vector2.Scale(viewportPosition, CanvasDimensions);
        Arrow.transform.up = arrowDirection;

        TextParent.transform.position = (Vector2) Arrow.transform.position + Vector2.Scale(-arrowDirection, TextOffset);
    }

    void setTexts ()
    {
        int distance = (int) Vector3.Distance(targetVisual.transform.position, Player.Instance.transform.position);
        string text = title + "\n" + distance + "M";

        FillText.text = text;
        LabelText.text = text;
    }
}
