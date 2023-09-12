using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    //public float minDistanceToShowTutorial = 2.0f; // Adjust this distance as needed.
    //public GameObject tutorialUI;
    //private bool tutorialShown = false;

    //private void Start()
    //{
    //    tutorialUI.SetActive(false);
    //}

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Obstacle") && !tutorialShown)
    //    {
    //        float distance = Vector2.Distance(transform.position, other.transform.position);

    //        if (distance <= minDistanceToShowTutorial)
    //        {
    //            ShowTutorial();
    //        }
    //    }
    //}

    //private void ShowTutorial()
    //{
    //    tutorialShown = true;
    //    Time.timeScale = 0f;

    //    tutorialUI.SetActive(true);
    //    Debug.Log("Tutorial showing");
    //}

    //private void CloseTutorial()
    //{
    //    Time.timeScale = 1f;
    //    tutorialUI.SetActive(false);
    //}

    public GameObject tutorialUI;
    public float obstacleDetectionDistance = 5f;
    [SerializeField] private bool showingTutorial = false;
    private PlayerController playerController;
    private bool hasShownTutorial = false;
    private bool detectedFirstObstacle = false;

    private bool isTimeScaleTransitioning = false;
    private float timeScaleTransitionDuration = .9f;

    private void Start()
    {
        playerController = PlayerController.Instance;

        if (PlayerPrefs.HasKey("TutorialCompleted"))
        {
            hasShownTutorial = PlayerPrefs.GetInt("TutorialCompleted") == 1;
        }
    }

    private void Update()
    {
        if (!hasShownTutorial && !showingTutorial)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, obstacleDetectionDistance);
            Debug.DrawRay(transform.position, Vector2.right * obstacleDetectionDistance, Color.red);

            if (hit.collider != null && hit.collider.GetComponentInChildren<BoxCollider2D>().CompareTag("Obstacle"))
            {
                ShowTutorialUI();
                detectedFirstObstacle = true;
            }
        }
        else if (showingTutorial)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began && touch.position.y > touch.deltaPosition.y)
                {
                    HideTutorialUI();
                    hasShownTutorial = true;
                }
            }
        }
    }

    private void ShowTutorialUI()
    {
        tutorialUI.SetActive(true);
        Time.timeScale = 0f; 
        showingTutorial = true;
    }
    private void HideTutorialUI()
    {
        if (isTimeScaleTransitioning)
        {
            return;
        }

        isTimeScaleTransitioning = true;
        StartCoroutine(SmoothlyIncreaseTimeScale());
    }

    private IEnumerator SmoothlyIncreaseTimeScale()
    {
        float startTimeScale = Time.timeScale;
        float endTimeScale = 1.0f;
        float elapsedTime = 0f;

        while (elapsedTime < timeScaleTransitionDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsedTime / timeScaleTransitionDuration);
            Time.timeScale = Mathf.Lerp(startTimeScale, endTimeScale, t);
            yield return null;
        }

        Time.timeScale = endTimeScale;
        isTimeScaleTransitioning = false;

        tutorialUI.SetActive(false);
    }
}
