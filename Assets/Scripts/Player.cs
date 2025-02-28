using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private Rigidbody rb;
    private bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Adding MovePlayer as a listener to the OnMove event
        inputManager.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Jump when Spacebar is pressed and only if grounded
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            JumpPlayer();
        }
    }

    private void MovePlayer(Vector3 direction)
    {
        Vector3 moveDirection = new(direction.x, 0f, direction.z);
        Vector3 targetPosition = rb.position + moveDirection * speed * Time.deltaTime;
        rb.MovePosition(targetPosition);
    }


    private void JumpPlayer()
    {
        // Apply upward force for jump
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true; // Player is on the floor
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false; // Player is no longer on the floor
        }
    }


}
