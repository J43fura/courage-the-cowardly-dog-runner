using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
	[SerializeField] private AudioSource jumpSound;


    private int desiredLane = 1; // 0: Left, 1: Middle, 2: Right
    public float laneDistance = 3; // The distance between two lanes
	public float gravity = -20f;
	public float jumpForce = 8f;
	public float fallMultiplier = 2.5f;   // faster fall

	private float verticalVelocity;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        direction.z = forwardSpeed;

        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
			desiredLane++;

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            desiredLane--;

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            Jump();

		if (Keyboard.current.downArrowKey.wasPressedThisFrame)
			getDown();

		Vector3 targetPosition = transform.position;

		switch (desiredLane)
		{
			case 0:
				targetPosition.x = -laneDistance;
				break;
			case 1:
				targetPosition.x = 0;
				break;
			case 2:
				targetPosition.x = laneDistance;
				break;
		}

		direction.x = (targetPosition.x - transform.position.x) * 10f; // lane change speed

		// Gravity
		if (controller.isGrounded)
		{
			if (verticalVelocity < 0)
				verticalVelocity = -2f; // stick to ground
		}
		else
		{
    	// Better gravity curve
			if (verticalVelocity > 0)
				verticalVelocity += gravity * Time.deltaTime;
			else
				verticalVelocity += gravity * fallMultiplier * Time.deltaTime;

		}

		direction.y = verticalVelocity;
    }

    private void FixedUpdate()
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

	private void getDown()
	{
		if (!controller.isGrounded)
		{
			verticalVelocity = -2*jumpForce;
		}
	}
}
