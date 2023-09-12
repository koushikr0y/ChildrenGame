using UnityEngine;
public enum PlayerState
{
    Running,
    Damaged,
    Jumping,
    Dragging
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Rigidbody2D _rb;
    private Animator _animator;
    [SerializeField] private Transform _groundCheckTransform;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private float _jumpForce = .05f;
    [SerializeField] private float _forwardJumpForce = 2.5f;
    [SerializeField] private float _groundRadius = .5f;

    public PlayerState playerState;

    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _hasJumped;
    private bool isJumping = false;

    public float movementSpeed = 0f;
    private float maxMovementSpeed = 6.3f;
    private float jumpTime = 0f;
    [SerializeField] private float jumpAnimationDuration = .01f;

    private Vector3 initialPosition;

    private const string SPEED = "speed";
    private const string IS_DRAGING = "isDrag";
    private const string IS_RUN = "isRun";
    private const string IS_JUMP = "isJump";
    private const string IS_DAMAGE = "isDamage";

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        playerState = PlayerState.Running;
    }


    private void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.GAMERUNNING)
        {
            UpdateMovementSpeed();
            _animator.SetFloat(SPEED, movementSpeed);

            float distanceTraveled = transform.position.x - initialPosition.x;
            float maxDistance = 300f;
            float sliderValue = Mathf.Clamp01(distanceTraveled / maxDistance);
            UIManager.Instance.UpdateDistanceSlider(sliderValue);

            if (playerState != PlayerState.Damaged)
            {
                CheckGround();
                _rb.velocity = new Vector2(movementSpeed, _rb.velocity.y);
                if (jumping() && _isGrounded && ((playerState == PlayerState.Running) || (playerState == PlayerState.Dragging)))
                {
                    isJumping = true;
                    _animator.SetBool(IS_JUMP, true);
                    playerState = PlayerState.Jumping;
                }
            }
        }
    }

    private void UpdateMovementSpeed()
    {
        movementSpeed = Mathf.Min(movementSpeed + 0.1f, maxMovementSpeed);
    }

    public void ChangeSpeed()
    {
        if(GameManager.Instance.gameState == GameManager.GameState.GAMERUNNING)
        {
            playerState = PlayerState.Damaged;
            _animator.SetBool(IS_DAMAGE, true);
            _animator.SetBool(IS_JUMP, false);
            movementSpeed = 0f;
        }
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheckTransform.position, _groundRadius, _groundLayer);
    }

    private bool jumping()
    {
        #region OLD
        //bool isJumping = false;

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    isJumping = true;
        //}

        //if (Input.touchCount > 0)
        //{
        //    Touch myTouch = Input.touches[0];
        //    if (myTouch.phase == TouchPhase.Began && myTouch.deltaPosition.y > 0)
        //    {
        //        isJumping = true;
        //    }
        //}
        //return isJumping;
        #endregion
        return Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Began /*&& Input.touches[0].deltaPosition.y > 0*/));
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheckTransform.position, _groundRadius);
    }

    public void StopJumping()
    {
        playerState = PlayerState.Running;
        _animator.SetBool(IS_JUMP, false);
        _animator.SetBool(IS_RUN, true);
    }

    public void DragingComplete()
    {
        playerState = PlayerState.Running;
        _animator.SetBool(IS_DRAGING, false);
    }

    public void removeDamageEffect()
    {
        playerState = PlayerState.Dragging;
        _animator.SetBool(IS_DAMAGE, false);
        _animator.SetBool(IS_DRAGING, true);
    }
    public void RunComplete()
    {
        playerState = PlayerState.Dragging;
        _animator.SetBool(IS_DRAGING, true);
    }
}