using UnityEngine;
using System;


public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        WAITINGTOSTART,
        COUNTDOWNTOSTART,
        GAMERUNNING,
        GAMEOVER,
    }

    public static GameManager Instance;

    [SerializeField] PlayerController player;
    public float countdownToStartTimer = 3f;

    [SerializeField] private GameState _state;
    public GameState gameState => _state;

    public Action GameStartAction;
    public Action GameRunningAction;
    public Action GameOverAction;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _state = GameState.WAITINGTOSTART;
        GameOverAction += GameOverEvent;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public bool IsCountDownToStartActive()
    {
        return _state == GameState.COUNTDOWNTOSTART;
    }

    public void GameStartEventExecuted()
    {
        GameStartAction?.Invoke();
        _state = GameState.COUNTDOWNTOSTART;
        //UIManager.Instance.ResetTimer();
    }

    public void GameRunningEvent()
    {
        _state = GameState.GAMERUNNING;
        player.GetComponent<Animator>().SetTrigger("Start");
        player.playerState = PlayerState.Running;
        GameRunningAction?.Invoke();
    }

    public void GameOverEvent()
    {
        _state = GameState.GAMEOVER;
        player.GetComponent<Animator>().SetTrigger("GameOver");
    }
}
