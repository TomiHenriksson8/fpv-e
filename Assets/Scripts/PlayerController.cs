using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce = 5f;
    public LayerMask groundMask;
    public Transform orientation;
    public Transform groundCheck;
    public float groundDistance = 0.5f;
    public TextMeshProUGUI goalText;

    private float xInput;
    private float yInput;
    private Vector3 movingDirection;
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;


        goalText.gameObject.SetActive(false);
    }

    void Update()
    {

        if (transform.position.y >= 30f && !goalText.gameObject.activeSelf)
        {
            goalText.gameObject.SetActive(true);
            Debug.Log("Goal Reached!");
        }


        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }


        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        movingDirection = orientation.forward * yInput + orientation.right * xInput;
        rb.AddForce(movingDirection.normalized * speed, ForceMode.Force);

        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (horizontalVelocity.magnitude > speed)
        {
            Vector3 limitedVelocity = horizontalVelocity.normalized * speed;
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
