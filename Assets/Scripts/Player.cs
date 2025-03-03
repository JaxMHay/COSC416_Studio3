using Unity.Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float dashForce = 100f;
    [SerializeField] public new CinemachineCamera camera;

    private Rigidbody rb;
    private bool isGrounded;
    private bool jumpedTwice;
    private bool hasDashed;

    void Start()
    {
        //add MovePlayer listener for OnMove
        inputManager.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //jumps when player is grounded or has only jumped once since leaving the ground
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            JumpPlayer();
        }
        else if (!isGrounded && !jumpedTwice && Input.GetKeyDown(KeyCode.Space))
        {
            JumpPlayer();
            jumpedTwice = true;
        }

        //dash when shift is pressed
        if(!hasDashed && Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
            hasDashed = true;
        }
    }

    private void MovePlayer(Vector3 direction)
    {
        //get inputs forhorizontal and vertical movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //get camera components
        Vector3 forward = camera.transform.forward;
        Vector3 right = camera.transform.right;

        //don't allow y-component of camera angle to affect movement
        forward.y = 0;
        right.y = 0;
        forward.Normalize(); //normalize to ensure constant speed
        right.Normalize();


        //calculate movement direction with respect to camera angle, move player accordingly
        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;
        Vector3 targetPosition = rb.position + moveDirection * speed * Time.deltaTime;
        rb.MovePosition(targetPosition);
    }


    private void JumpPlayer()
    {
        //apply jump force
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    private void Dash()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        
        Vector3 forward = camera.transform.forward;
        Vector3 right = camera.transform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();


        //same as code for movePlayer(), but with dashForce component added to simulate a burst of movement
        Vector3 moveDirection = dashForce * (forward * vertical + right * horizontal).normalized;
        Vector3 targetPosition = rb.position + moveDirection * speed * Time.deltaTime;
        rb.MovePosition(targetPosition);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true; //player is on the floor
            jumpedTwice = false; //reset double jump
            hasDashed = false; //reset dash
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false; //player is in the air
        }
    }


}
