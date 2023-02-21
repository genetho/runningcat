using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGrounded;
    [SerializeField]
    float jumpForce = 4.5f;
    [SerializeField]
    float moveSpeed = 5f;
    
    public Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        rb.velocity = Vector2.zero;
        float respawnX = Camera.main.ScreenToWorldPoint(Vector3.zero).x + 2f;
        transform.position = new Vector3(respawnX, -2.2f, 0f);
    }

    private void Update()
    {
        // Get the screen boundaries
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // Clamp the position of the player within the screen boundaries
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minScreenBounds.x, maxScreenBounds.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minScreenBounds.y, maxScreenBounds.y);
        transform.position = clampedPosition;
    }

    private void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (isGrounded && Input.GetAxis("Vertical") > 0)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
