using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatedObject : MonoBehaviour
{
    public Transform ObjectToIndicate;
    public string Title;

    IndicatorArrow arrow;

    void Start ()
    {
        arrow = IndicatorArrowFactory.Instance.SpawnArrow(ObjectToIndicate, Title);
    }

    void OnDestroy ()
    {
        if (arrow != null && arrow.gameObject != null) Destroy(arrow.gameObject);
    }
}
