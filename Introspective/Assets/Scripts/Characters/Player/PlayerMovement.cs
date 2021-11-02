using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour {

    #region Variables
    [Header("General Settings")]
	[Space]
	public float slowMoveSpeed = 15f;
	public float moderateMoveSpeed = 25f;
	public float standardMoveSpeed = 40f;
	public float fastMoveSpeed = 55f;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement

	[Space]
	
	public float jumpPower = 400f;
	float JumpTimeCounter = 1f;
	public float jumpTime;
	public int jumpCountDefault = 1;
	public float airControl = 1f; [Range(0f, 1f)]
	private float limitFallSpeed = 25f; // Limit fall speed

	[HideInInspector] public Vector3 velocity = Vector3.zero;
	[HideInInspector] public float moveSpeed = 40f;

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.

	float horizontalMove = 0f;
	private int direction = 0;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.

	[Header ("Dash Settings")]
	[Space]
	public float dashSpeed = 25f;

	[Header ("Ability Checks")]
	[Space]
	public bool jumpAvailable = false;
	public bool dashAvailable = false;

	//Can Checks
	[HideInInspector] public bool canMove = true;
	public int jumpCount;
	[HideInInspector] public bool canDash = true;
	[HideInInspector] public bool canJump = true;
	private bool isDashing = false;
	private bool isJumping = false;

	[Header("Requirements")]
	[Space]
	public CharacterController2D controller;
	[HideInInspector] public Rigidbody2D body;
	public Animator animator;

	[Space]
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_WallCheck;                             //Posicion que controla si el personaje toca una pared
	private bool canCheckGround = true;

	[Space]
	public SpriteRenderer phase1;
	public SpriteRenderer phase2;
	public SpriteRenderer phase3;
	public SpriteRenderer focusSprite;
	[Space]
	public ParticleSystem particleJumpUp; //Trail particles
	public ParticleSystem particleJumpDown; //Explosion particles

	[Header ("Events")]
	[Space]
	public UnityEvent OnFallEvent;
	public UnityEvent OnLandEvent;
	#endregion


	private void Start()
	{
		JumpTimeCounter = jumpTime;
		jumpCount = jumpCountDefault;
		body = this.GetComponent<Rigidbody2D>();
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
			if (canCheckGround == false)
            {
				break;
            }

			if (colliders[i].gameObject != gameObject)
            {
				m_Grounded = true;
			}

			if (!wasGrounded)
			{
				OnLandEvent.Invoke();
				if (!isDashing)
					particleJumpDown.Play();
				//canDoubleJump = true;
			}
		}

		if (!m_Grounded)
		{
			OnFallEvent.Invoke();
			//prevVelocityX = body.velocity.x;
		}

		horizontalMove = direction * moveSpeed;

		// Move our character
		if (isJumping)
        {
			Jump();
        }

		Move();
		isDashing = false;
	}

	// Update is called once per frame
	void Update()
	{
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
	}

    private void Move()
    {
		if (body.velocity.y < -limitFallSpeed)
		{
			body.velocity = new Vector2(body.velocity.x, -limitFallSpeed);
		}

		if (canMove)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2((horizontalMove * Time.fixedDeltaTime) * 10f, body.velocity.y);
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

		// If the input is moving the player right and the player is facing left...
		if (horizontalMove > 0 && !m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (horizontalMove < 0 && m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void ChangeMoveSpeed(string newSpeed)
    {
		float fSpeed = 0f;

		switch (newSpeed)
        {
			case "Slow":
				fSpeed = slowMoveSpeed;
				break;

			case "Moderate":
				fSpeed = moderateMoveSpeed;
				break;

			case "Standard":
				fSpeed = standardMoveSpeed;
				break;

			case "Fast":
				fSpeed = fastMoveSpeed;
				break;
        }

		moveSpeed = fSpeed;
    }

	public void SetMoveDirection(int _direction)
    {
		direction = _direction;

		SpriteRenderer sprite = focusSprite;

		switch (_direction)
        {
			case 1:
				if (sprite.flipX == true)
					sprite.flipX = false;
				break;

			case -1:
				if (sprite.flipX == false)
					sprite.flipX = true;
				break;
        }
    }


	public void ToggleJump(bool value)
    {
		if (isJumping == true && value == false)
        {
			ResetJumpTimer();
			jumpCount -= 1;
		}

		isJumping = value;
	}

	public void Jump()
    {
		if (jumpCount <= 0)
		{
			return;
		}

		// If the player should jump...
		if (m_Grounded)
		{
			// Add a vertical force to the player.
			animator.SetBool("IsJumping", true);
			animator.SetBool("JumpUp", true);

			m_Grounded = false;
			body.AddForce(new Vector2(0f, jumpPower));

			particleJumpDown.Play();
			particleJumpUp.Play();
		}
		else if (!m_Grounded)
		{
			body.velocity = new Vector2(body.velocity.x, 0);
			body.AddForce(new Vector2(0f, jumpPower));
			animator.SetBool("IsDoubleJumping", true);
		}

		if (JumpTimeCounter > 0)
        {
			JumpTimeCounter -= Time.deltaTime;
        }
		else
        {
			ToggleJump(false);
        }

	}


	private void Dash()
    {
		isDashing = true;
		#region Dashing Movement
		if (canDash)
		{
			//m_Rigidbody2D.AddForce(new Vector2(transform.localScale.x * m_DashForce, 0f));
			StartCoroutine(DashCooldown());
		}

		// If crouching, check to see if the character can stand up
		if (isDashing)
		{
			body.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);
		}
		#endregion
	}


	public void OnFall()
	{
		animator.SetBool("IsJumping", true);
	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
		ResetJump();
	}


	public void ChangeSprite(int phase)
	{
		switch (phase)
		{
			case 1:
				phase1.enabled = true;
				phase2.enabled = false;
				phase3.enabled = false;

				focusSprite = phase1;
				break;

			case 2:
				phase1.enabled = false;
				phase2.enabled = true;
				phase3.enabled = false;

				focusSprite = phase2;
				break;

			case 3:
				phase1.enabled = false;
				phase2.enabled = false;
				phase3.enabled = true;

				focusSprite = phase3;
				break;
		}
	}


	public void ResetJump()
    {
		ResetJumpTimer();
		jumpCount = jumpCountDefault;
		canJump = true;
    }

	public void ResetJumpTimer()
    {
		JumpTimeCounter = jumpTime;
    }

	IEnumerator GroundCheckDisable()
	{
		canCheckGround = false;
		yield return new WaitForSeconds(0.1f);
		canCheckGround = true;
	}

	IEnumerator DashCooldown()
	{
		animator.SetBool("IsDashing", true);
		isDashing = true;
		canDash = false;
		yield return new WaitForSeconds(0.1f);
		isDashing = false;
		yield return new WaitForSeconds(0.5f);
		canDash = true;
	}
}
