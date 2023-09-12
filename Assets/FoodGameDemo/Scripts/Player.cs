
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Animator animator;
    public GameObject _particleAnimator;
    private const string IsHealthy = "Healthy";
    private const string IsUnHealthy = "UnHealthy";
    private const string IsIdle = "Idle";

    [SerializeField] private Food currentFood;

    private bool isEating;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayHealthyEatAnim()
    {
        animator.SetBool(IsHealthy, true);
        animator.SetBool(IsUnHealthy, false);
        isEating = true;
    }

    public void PlayUnHealhtyEatAnim()
    {
        animator.SetBool(IsUnHealthy, true);
        animator.SetBool(IsHealthy, false);
        isEating = true;

    }

    public void PlayIdleAnim()
    {
        animator.SetTrigger(IsIdle);
        animator.SetBool(IsHealthy, false);
        animator.SetBool(IsUnHealthy, false);
        isEating = false;

    }

    public void OnAnimationEnd()
    {

        _particleAnimator.SetActive(false);
        PlayIdleAnim();

        if (currentFood != null)
        {
            Destroy(currentFood.gameObject);
            currentFood = null;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            Food foodItem = other.GetComponent<Food>();

            if (foodItem != null)
            {
                if (foodItem.foodType == FOODTYPE.HEALTHY)
                {
                    PlayHealthyEatAnim();
                    _particleAnimator.SetActive(true);
                    _particleAnimator.GetComponent<Image>().color = foodItem.foodsColor;
                }
                else
                {
                    PlayUnHealhtyEatAnim();
                    _particleAnimator.SetActive(true);
                    _particleAnimator.GetComponent<Image>().color = foodItem.foodsColor;
                }
            }
            FoodUIManager.Instance.SetTimerValue(10f);
            Debug.Log("Meter Call");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //PlayIdleAnim();
        //_particleAnimator.SetActive(false);

        OnAnimationEnd();
    }
}
