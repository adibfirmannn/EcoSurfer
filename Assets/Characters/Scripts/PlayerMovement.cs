using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 5f;        // Kecepatan maju
    public float laneDistance = 3f;         // Jarak antar jalur
    public float laneSwitchSpeed = 10f;     // Kecepatan pindah jalur
    public float sideOffset = 1f;           // Koreksi posisi samping
    public float jumpForce = 7f;            // Kekuatan loncat
    public float gravityModifier;

    private int targetLane = 1;             // 0 = kiri, 1 = tengah, 2 = kanan
    private Rigidbody rb;
    private Animator anim;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
    }

    void Update()
    {
        MoveForward();
        HandleLaneSwitch();
        HandleJump();
        UpdateAnimationStates();
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
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            isGrounded = false;
            anim.SetTrigger("JumpTrig"); // Set animasi jump true
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
    }

    void UpdateAnimationStates()
    {
        if (isGrounded)
        {
            anim.SetBool("isJumping", false); // Balik ke run saat mendarat
        }
    }

    // Cek menyentuh tanah pakai TAG
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
