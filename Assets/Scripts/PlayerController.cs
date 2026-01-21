using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    [SerializeField] private AudioSource jumpSound;

    // Lanes setup
    private readonly float[] lanes = { -3f, 0f, 3f }; // x positions for lanes
    private int currentLane = 1; // middle lane

    public float gravity = -20f;
    public float jumpForce = 8f;
    public float fallMultiplier = 2.5f;

    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        direction.z = forwardSpeed;

        // Handle lane input
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            currentLane = Mathf.Clamp(currentLane + 1, 0, lanes.Length - 1);
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            currentLane = Mathf.Clamp(currentLane - 1, 0, lanes.Length - 1);

        // Jump & Down
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            Jump();
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            GetDown();

        // Smooth lane movement
        float targetX = lanes[currentLane];
        direction.x = (targetX - transform.position.x) * 10f;

        // Gravity
        if (controller.isGrounded)
        {
            if (verticalVelocity < 0)
                verticalVelocity = -2f; // stick to ground
        }
        else
        {
            verticalVelocity += (verticalVelocity > 0 ? gravity : gravity * fallMultiplier) * Time.deltaTime;
        }

        direction.y = verticalVelocity;
    }

    void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = jumpForce;
            jumpSound.Play();
        }
    }

    private void GetDown()
    {
        if (!controller.isGrounded)
        {
            verticalVelocity = -2 * jumpForce;
        }
    }
}
