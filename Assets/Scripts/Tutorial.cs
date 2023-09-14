using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private PlayerController playerController;

    public GameObject tutorialUI;
    public float obstacleDetectionDistance = 5f;
    
    [SerializeField] private bool showingTutorial = false;
    private bool hasShownTutorial = false;
    private bool detectedFirstObstacle = false;
    private bool isTimeScaleTransitioning = false;
    private bool canJump = false;

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
                canJump = true;
                playerController.canMove = false;
            }
        }
        else if (showingTutorial)
        {
            if (canJump && Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began && touch.position.y > touch.deltaPosition.y)
                {
                    HideTutorialUI();
                    hasShownTutorial = true;
                    canJump = false;                                                
                    playerController.canMove = true;
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
        Time.timeScale = 1f;
        tutorialUI.SetActive(false);
        //StartCoroutine(SmoothlyIncreaseTimeScale());
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
