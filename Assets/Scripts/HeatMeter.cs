using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatMeter : MonoBehaviour
{
    public Player Player;

    public AnimationCurve HeatToTopLabelAlpha, HeatToBottomLableAlpha;

    public CanvasGroup TopLabel, BottomLabel;
    public Image Bar;

    void Update ()
    {
        Bar.fillAmount = Player.Heat.NormalizedValue;

        float heat = Player.Heat.CurrentValue;

        TopLabel.alpha = HeatToTopLabelAlpha.Evaluate(heat);
        BottomLabel.alpha = HeatToBottomLableAlpha.Evaluate(heat);
    }
}
