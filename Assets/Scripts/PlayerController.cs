using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

    private int desiredLane = 1; // 0: Left, 1: Middle, 2: Right
    public float laneDistance = 4; // The distance between two lanes
	public float gravity = -20f;
	private float verticalVelocity;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        direction.z = forwardSpeed;

        if (Keyboard.current.rightArrowKey.wasPressedThisFrame && desiredLane < 2)
			desiredLane++;

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame && desiredLane > 0)
            desiredLane--;

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
			verticalVelocity += gravity * Time.deltaTime;
		}

		direction.y = verticalVelocity;
    }

    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }
}
