using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStartCountDownUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopUp";

    [SerializeField] private TextMeshProUGUI countdownText;
    private Animator animator;
    private int prevCountDwonNumber;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        GameManager.Instance.GameStartAction += KitchenGameManager_OnStateChanged;
    }
    private void KitchenGameManager_OnStateChanged()
    {
        if (GameManager.Instance.IsCountDownToStartActive())
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Update()
    {
        int countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownToStartTimer());
        countdownText.text = countdownNumber.ToString();

         
        if(prevCountDwonNumber != countdownNumber)
        {
            prevCountDwonNumber = countdownNumber;
            animator.SetTrigger(NUMBER_POPUP);
        }
        else
        {
            //countdownText.text = "GO";
            //animator.SetTrigger(NUMBER_POPUP);
            //gameObject.SetActive(false);
            //GameManager.Instance.GameRunningEvent();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
