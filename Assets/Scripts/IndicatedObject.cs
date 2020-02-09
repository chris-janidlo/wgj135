using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatedObject : MonoBehaviour
{
    public Renderer RendererToIndicate;
    public string Title;

    IndicatorArrow arrow;

    void Start ()
    {
        arrow = IndicatorArrowFactory.Instance.SpawnArrow(RendererToIndicate, Title);
    }

    void OnDestroy ()
    {
        if (arrow != null && arrow.gameObject != null) Destroy(arrow.gameObject);
    }
}
