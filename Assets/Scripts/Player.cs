using Unity.Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] public new CinemachineCamera camera;

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
        // Get inputs for WASD (Horizontal and Vertical movement)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Get the camera's components to align the movement with the camera direction
        Vector3 forward = camera.transform.forward;
        Vector3 right = camera.transform.right;

        forward.y = 0; // Prevent the vertical movement from affecting player movement
        right.y = 0; // Prevent the vertical movement from affecting player movement
        forward.Normalize(); // Normalize to ensure constant speed
        right.Normalize();



        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;
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
