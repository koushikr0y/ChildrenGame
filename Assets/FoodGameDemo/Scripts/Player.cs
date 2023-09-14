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

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayHealthyEatAnim()
    {
        animator.SetBool(IsHealthy, true);
        //animator.SetBool(IsUnHealthy, false);
    }

    public void PlayUnHealhtyEatAnim()
    {
        animator.SetBool(IsUnHealthy, true);
        //animator.SetBool(IsHealthy, false);
    }

    public void PlayIdleAnim()
    {
        animator.SetTrigger(IsIdle);
        animator.SetBool(IsHealthy, false);
        animator.SetBool(IsUnHealthy, false);
    }

    public void OnAnimationEnd()
    {

        _particleAnimator.SetActive(false);
        PlayIdleAnim();
    }

    public void DestroyFood()
    {
        if (currentFood != null)
        {
            Destroy(currentFood.gameObject);
            currentFood = null;
            FoodContainer.Instane.spawnNewFood?.Invoke();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            Food foodItem = other.GetComponent<Food>();
            currentFood = foodItem;
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
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //OnAnimationEnd();
    }
}
