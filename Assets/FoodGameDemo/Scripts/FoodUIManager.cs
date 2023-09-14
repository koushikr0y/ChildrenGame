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
        
        LeanTween.rotateZ(pedal.gameObject, rotationAngle, 0.5f).setEase(LeanTweenType.easeOutQuad);

        if (clampValue >= 178)
        {
            LeanTween.scale(gameOver, Vector3.one, .6f).setEase(LeanTweenType.easeInOutSine).setOnUpdate((float val) =>
            {
                    LeanTween.scale(playAgainButton.gameObject, Vector3.one, .5f).setEase(LeanTweenType.easeOutQuad);
                    LeanTween.scale(TextGameOver.gameObject, Vector3.one, .5f).setEase(LeanTweenType.easeOutQuad);
            });

            LeanTween.scale(ui1.gameObject, Vector3.one, .5f).setLoopPingPong();
            LeanTween.scale(ui2.gameObject, Vector3.one, .5f).setLoopPingPong(99);
        }
    }
}
