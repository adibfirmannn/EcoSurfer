using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 5f;
    public float laneDistance = 3f;
    public float laneSwitchSpeed = 5f;
    public float sideOffset = 1f;
    public float jumpForce = 7f;
    public float gravityModifier = 1f;

    [Header("Ground Check")]
    public Transform groundChecker;
    public float groundCheckRadius = 0.3f;
    public LayerMask groundLayer;

    private int targetLane = 1; // 0 = kiri, 1 = tengah, 2 = kanan
    private Rigidbody rb;
    private Animator anim;
    private bool isGrounded;
    private bool isGameOver = false;

    private int lastScoreCheckpoint = 0;
    public int scoreStep = 100;
    public float speedIncrement = 1f;

    // Roll
    private bool isRolling = false;
    public float rollDuration = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;

        if (groundChecker == null)
        {
            Debug.LogWarning("GroundChecker is not assigned.");
        }
    }

    void Update()
    {
        if (isGameOver) return;

        MoveForward();
        HandleLaneSwitch();
        HandleJump();
        HandleRoll();
        CheckGrounded();
        UpdateAnimationStates();
        CheckSpeedIncrease();
    }

    void FixedUpdate()
    {
        // Optional
        // CheckGrounded();
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }

    void HandleLaneSwitch()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            targetLane--;
            targetLane = Mathf.Max(0, targetLane);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            targetLane++;
            targetLane = Mathf.Min(2, targetLane);
        }

        float xPos = (targetLane - 1) * laneDistance;

        if (targetLane == 0)
            xPos += sideOffset;
        else if (targetLane == 2)
            xPos -= sideOffset;

        Vector3 targetPosition = new Vector3(xPos, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * laneSwitchSpeed);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isRolling)
        {
            anim.ResetTrigger("JumpTrig");
            anim.SetTrigger("JumpTrig");
            Jump();
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // Reset Y velocity
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void HandleRoll()
    {
        if (Input.GetKeyDown(KeyCode.S) && isGrounded && !isRolling)
        {
            StartCoroutine(Roll());
        }
    }

    IEnumerator Roll()
    {
        isRolling = true;
        anim.SetTrigger("RollTrig");

        CapsuleCollider col = GetComponent<CapsuleCollider>();
        float originalHeight = col.height;
        Vector3 originalCenter = col.center;

        // Perkecil collider agar bisa melewati rintangan bawah
        col.height = originalHeight / 2;
        col.center = new Vector3(col.center.x, col.center.y - originalHeight / 4, col.center.z);

        yield return new WaitForSeconds(rollDuration);

        // Kembalikan collider ke ukuran semula
        col.height = originalHeight;
        col.center = originalCenter;

        isRolling = false;
    }

    void CheckGrounded()
    {
        if (groundChecker != null)
        {
            isGrounded = Physics.CheckSphere(groundChecker.position, groundCheckRadius, groundLayer);
        }
        else
        {
            isGrounded = false;
        }
    }

    void UpdateAnimationStates()
    {
        anim.SetBool("isJumping", !isGrounded);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isGameOver = true;
            anim.SetTrigger("DeathTrig");
            forwardSpeed = 0;
            rb.linearVelocity = Vector3.zero;
            ScoreManager.Instance.StopScoring();
        }
    }

    void CheckSpeedIncrease()
    {
        int currentScore = ScoreManager.Instance.GetScore();
        if (currentScore >= lastScoreCheckpoint + scoreStep)
        {
            forwardSpeed += speedIncrement;
            lastScoreCheckpoint = currentScore;
            Debug.Log("Speed increased! Current Speed: " + forwardSpeed);
        }
    }
}
