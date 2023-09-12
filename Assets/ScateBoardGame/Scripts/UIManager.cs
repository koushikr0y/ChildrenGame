using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopUp";
    public static UIManager Instance;

    [SerializeField] private TMP_Text countDownText;
    [SerializeField] private TMP_Text distanceTraveledText;
    [SerializeField] private TMP_Text TextGameOver;

    [SerializeField] private Image gameOverUI;
    //game over
    [SerializeField] private TMP_Text scoreText;

    [Header("GAME OVER")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button playAgainButton;

    [SerializeField] private Animator countDownAnimator;
    [SerializeField] private Slider distanceSlider;
    //[SerializeField] private ParticleSystem _particleSystem;

    private float startTime;
    private bool isTimerRunning;
    private bool timerStarted;

    private void Awake()
    {
        Instance = this;    
    }

    private void Start()
    {
        GameManager.Instance.GameStartAction += HandleGameStart;
        GameManager.Instance.GameRunningAction += InitializeTimer;
        GameManager.Instance.GameOverAction += StopTimer;

        homeButton.onClick.AddListener(() => Loader.Load(Loader.Scene.MainMenu));
        playAgainButton.onClick.AddListener(() => Loader.Load(Loader.Scene.SkateGame));


        distanceSlider.onValueChanged.AddListener(UpdateDistanceSlider);
    }

    public void UpdateDistanceSlider(float sliderValue)
    {
        //float maxDistance = 1000.0f;
        //float distance = maxDistance * sliderValue;
        //distanceTraveledText.text = distance.ToString("F2") + " m";

        distanceSlider.value = sliderValue;
        float distance = sliderValue * 100;
        distanceTraveledText.text = distance.ToString("00") + "m";
    }

    private void HandleGameStart()
    {
        StartCoroutine(StartCountDown());
    }

    private IEnumerator StartCountDown()
    {
        for(int i = 3; i > 0; i--)
        {
            countDownText.text = i.ToString();
            countDownAnimator.SetTrigger(NUMBER_POPUP);
            yield return new WaitForSeconds(1.1f);
        }
        countDownText.text = "GO";
        yield return new WaitForSeconds(.5f);
        countDownText.enabled = false;

        GameManager.Instance.GameRunningEvent();
    }

    private void Update()
    {
        if (isTimerRunning && GameManager.Instance.gameState == GameManager.GameState.GAMERUNNING)
        {
            if (!timerStarted)
            {
                startTime = 0f;
                timerStarted = true;
            }

            startTime += Time.deltaTime;
            UpdateScoreText(startTime);
        }
    }


    private void InitializeTimer()
    {
        startTime = 0f;
        isTimerRunning = true;
    }

    //private void UpdateTimerText(float elapsedTime)
    //{
    //    //int minutes = Mathf.FloorToInt(elapsedTime / 60);
    //    //int seconds = Mathf.FloorToInt(elapsedTime % 60);
    //    //timerText.text = minutes.ToString("0") + ":" + seconds.ToString("00");

    //    //distanceTraveledText.text = elapsedTime.ToString("00") + " m";
    //}

    private void UpdateScoreText(float score)
    {
        scoreText.text = "Score: " + score.ToString("00");
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        countDownText.gameObject.SetActive(false);
        LeanTween.scale(gameOverPanel, Vector3.one, .5f).setEase(LeanTweenType.easeOutQuad).setOnUpdate((float val) =>
        {
            LeanTween.sequence().append(() =>
            {
                LeanTween.scale(playAgainButton.gameObject, Vector3.one, 1f).setEase(LeanTweenType.easeOutQuad);
            })
            .append(() =>
            {
                LeanTween.scale(TextGameOver.gameObject, Vector3.one, 1f).setEase(LeanTweenType.easeOutQuad)
                .setOnComplete(() =>
                {
                    LeanTween.scale(gameOverUI.gameObject, Vector3.one, .7f);
                });
            });
        });
    }

    public void ResetTimer()
    {
        startTime = 0;
        distanceTraveledText.text = "0:00";
    }

    private void OnDisable()
    {
        GameManager.Instance.GameStartAction -= HandleGameStart;
        GameManager.Instance.GameRunningAction -= InitializeTimer;
    }

    //public void PlayeParticleEffect()
    //{
    //    _particleSystem.Play();
    //}
}
