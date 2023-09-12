//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FoodUIManager : MonoBehaviour
{
    public static FoodUIManager Instance;

    [SerializeField] private Button homeButton;
    [SerializeField] private Button playAgainButton;

    private float maxRotation = 179f;

    [SerializeField] private float meterDecrementRate = 1f;

    public RectTransform pedal;

    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject meterbox;
    public Image ui1;
    public Image ui2;
    //[SerializeField] private RectTransform newPos;
    [SerializeField] private TMPro.TMP_Text TextGameOver;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        homeButton.onClick.AddListener(() => Loader.Load(Loader.Scene.MainMenu));
        playAgainButton.onClick.AddListener(() => Loader.Load(Loader.Scene.FoodGame));
    }

    float clampValue;
    public void SetTimerValue(float timerValue)
    {
        clampValue = Mathf.Clamp(timerValue + clampValue, 0.0f, maxRotation);
        float rotationAngle = 90 - clampValue;
        Debug.Log("clamp Angle :" + clampValue);
        LeanTween.rotateZ(pedal.gameObject, rotationAngle, 0.5f).setEase(LeanTweenType.easeOutQuad);

        if (clampValue >= 178)
        {
            LeanTween.scale(gameOver, Vector3.one, 1f).setEase(LeanTweenType.easeInOutSine).setOnComplete(() =>
            {
                LeanTween.sequence().append(() =>
                {
                    LeanTween.scale(playAgainButton.gameObject, Vector3.one, 1f).setEase(LeanTweenType.easeOutQuad)
                    .setOnComplete(() =>
                    {
                        //LeanTween.moveLocalX(playAgainButton.gameObject, playAgainButton.transform.localPosition.x - 20f, 0.25f)
                        //    .setEase(LeanTweenType.easeOutQuad)
                        //    .setOnComplete(() =>
                        //    {
                        //        LeanTween.moveLocalX(playAgainButton.gameObject, playAgainButton.transform.localPosition.x + 20f, 0.25f)
                        //            .setEase(LeanTweenType.easeOutQuad);
                        //    });
                    });
                })
                .append(() =>
                {
                    LeanTween.scale(TextGameOver.gameObject, Vector3.one, 1f).setEase(LeanTweenType.easeOutQuad)
                    .setOnComplete(() =>
                    {
                        LeanTween.scale(ui1.gameObject, Vector3.one, .7f).setLoopPingPong();
                        LeanTween.scale(ui2.gameObject, Vector3.one, .7f).setLoopPingPong();
                    });
                });
            });

            //LeanTween.scale(meterbox, new Vector3(1.2f, 1.2f, 1.2f), 1f).setEase(LeanTweenType.easeOutQuad);
            //LeanTween.move(meterbox, newPos, 1f).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
            //{
            //    meterbox.transform.SetParent(newPos);
            //    meterbox.transform.localPosition = Vector3.zero;
            //    gameOver.transform.GetChild(0).gameObject.SetActive(true);
            //    LeanTween.alphaCanvas(TextGameOver.GetComponent<CanvasGroup>(), 1f, 1f);
            //});
        }
    }
}
