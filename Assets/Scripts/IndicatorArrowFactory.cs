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

    public IndicatorArrow SpawnArrow (Transform targetVisual, string title)
    {
        var arrow = Instantiate(IndicatorArrowPrefab, transform);
        arrow.Initialize(targetVisual, title);
        return arrow;
    }
}
