using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFood : MonoBehaviour
{
    public CanvasGroup selfObj;
    public CanvasGroup gameObj;
    public void HideTutorial()
    {
            gameObj.gameObject.SetActive(true);
            LeanTween.alphaCanvas(gameObj, 1, 1f).setOnComplete(()=>selfObj.gameObject.SetActive(false));
 
    }
}
