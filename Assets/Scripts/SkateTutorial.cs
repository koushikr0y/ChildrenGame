using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkateTutorial : MonoBehaviour
{
    public GameObject ui;
    public float fadeDuration = 1f;

    public void HideTutorial()
    {
        //CanvasGroup cg = ui.GetComponent<CanvasGroup>();

        GameManager.Instance.GameStartEventExecuted();
        ui.gameObject.SetActive(false);
        //LeanTween.alphaCanvas(cg, 0.0f, fadeDuration).setOnComplete(() =>
        //{
        //    cg.gameObject.SetActive(false);
        //});
    }

    private IEnumerator FadeOutUI()
    {
        Image image = ui.GetComponent<Image>();

        float startAlpha = image.color.a;
        float targetAlpha = 0.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            Color newColor = image.color;
            newColor.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            image.color = newColor;
            yield return null;
        }
        Color finalColor = image.color;
        finalColor.a = targetAlpha;
        image.color = finalColor;
        ui.SetActive(false);
    }
}
