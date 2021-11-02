using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCMovement : MonoBehaviour
{
	
	[Header("Move Speed Presets")]
	[Space]
	public float FitMoveSpeed = 10f;
	public float FitWalkDuration = 15f;
	public float FitIdleDuration = 15f;

	public float AverageMoveSpeed = 5f;
	public float AverageWalkDuration = 5f;
	public float AverageIdleDuration = 5f;

	public float OutOfShapeMoveSpeed = 3f;
	public float OutOfShapeWalkDuration = 3f;
	public float OutOfShapeIdleDuration = 3f;

	float moveSpeed = 10f;
	float idleDuration = 5f;
	float walkDuration = 5f;
	public int direction = 1; // -1 = Left | 1 = Right
	public Vector3 velocity;

	[Header("Stuff I Gotta Sort")]
	[Space]
	private float limitFallSpeed = 25f; // Limit fall speed
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement

	public bool canMove = false;

	private bool m_FacingRight = true;  // For determining which way the player is currently facing.

	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.


	[Header("Required References")]
	[Space]
	public Timer idleTimer;
	public Timer walkTimer;

	[HideInInspector] public Rigidbody2D body;
	[SerializeField] private Transform m_GroundCheck;
	public GameObject boundaryCheck;
	Animator animator;


	[Header("Events")]
	[Space]
	public UnityEvent OnFallEvent;
	public UnityEvent OnLandEvent;


	// Start is called before the first frame update
	void Start()
	{
		body = this.GetComponent<Rigidbody2D>();

		this.transform.localScale = new Vector2(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y);
	}

	//This sets the movement speed, idle duration; and walk duration
	public void SetMoveSpeed(string speedPreset)
    {
		switch (speedPreset)
        {
			case "Fit":
				moveSpeed = FitMoveSpeed;
				idleDuration = FitIdleDuration;
				walkDuration = FitWalkDuration;
				break;

			case "Average":
				moveSpeed = AverageMoveSpeed;
				idleDuration = AverageIdleDuration;
				walkDuration = AverageWalkDuration;
				break;

			case "OutOfShape":
				moveSpeed = OutOfShapeMoveSpeed;
				idleDuration = OutOfShapeIdleDuration;
				walkDuration = OutOfShapeWalkDuration;
				break;
        }

		walkTimer.timerDuration = walkDuration;
		idleTimer.timerDuration = idleDuration;
    }


	public void StartWalking()
    {
		canMove = true;
		walkTimer.StartTimer();
    }

	public void StartIdle()
    {
		canMove = false;
		idleTimer.StartTimer();
    }


	void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
			}

			if (!wasGrounded)
			{
				OnLandEvent.Invoke();
			}
		}

		if (!m_Grounded)
		{
			OnFallEvent.Invoke();
		}

		velocity = new Vector2(direction * moveSpeed, body.velocity.y);

		Move();
	}

	//This makes the NPC move around
	private void Move()
	{
        #region Falling Code
        if (body.velocity.y < -limitFallSpeed)
		{
			body.velocity = new Vector2(body.velocity.x, -limitFallSpeed);
		}
        #endregion

        #region Movement Code
        if (canMove)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2((velocity.x * Time.fixedDeltaTime) * 10f, body.velocity.y);
			// And then smoothing it out and applying it to the character

			if (m_Grounded)
			{
				body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref velocity, m_MovementSmoothing);
			}
			else
			{
				body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref velocity, m_MovementSmoothing);
			}
		}
        #endregion

        #region Facing Logic
        // If the input is moving the player right and the player is facing left...
        /*
		if (velocity.x > 0 && transform.localScale.x == -1)
		{
			Flip(1);
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (velocity.x < 0 && transform.localScale.x == 1)
		{
			Flip(-1);
		}
		*/
        #endregion
    }

    #region Turning Functions
    //This will turn the NPC to face whatever they need to face
    private void Flip(int newDirection)
	{
		if (direction == newDirection)
        {
			print("Same direction");
			return;
        }

		print("Flip Direction to " + newDirection.ToString());

		// Switch the way the player is labelled as facing.
		direction = newDirection;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x = direction;
		transform.localScale = theScale;
	}

	//This turns around the NPC from whatever direction they're moving in
	public void TurnAround()
	{
		Flip(direction *= -1);
		StartCoroutine("TurnOnBoundaryCheck");
	}

	//This reactivates the boundary check
	IEnumerator TurnOnBoundaryCheck()
	{
		boundaryCheck.SetActive(false);
		yield return new WaitForSeconds(0.7f);
		boundaryCheck.SetActive(true);
	}
    #endregion
}
