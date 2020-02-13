using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class IndicatorArrowFactory : Singleton<IndicatorArrowFactory>
{
    public IndicatorArrow IndicatorArrowPrefab;

    void Awake ()
    {
        SingletonSetInstance(this, true);
    }

    void Update ()
    {
        List<IndicatorArrow> children = transform.GetComponentsInChildren<IndicatorArrow>().ToList();
        transform.DetachChildren();
        children.Sort((a1, a2) => a2.DistanceFromTarget - a1.DistanceFromTarget);
        foreach (IndicatorArrow child in children)
        {
            child.transform.SetParent(transform);
        }
    }

    public IndicatorArrow SpawnArrow (Transform targetVisual, string title)
    {
        var arrow = Instantiate(IndicatorArrowPrefab, transform);
        arrow.Initialize(targetVisual, title);
        return arrow;
    }
}
