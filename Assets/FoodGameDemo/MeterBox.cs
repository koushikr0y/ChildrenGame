using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterBox : MonoBehaviour
{
    public RectTransform pedal;
    private void OnEnable()
    {
        LeanTween.rotateZ(pedal.gameObject,-89f,2f).setEase(LeanTweenType.easeOutQuad);
    }
}
