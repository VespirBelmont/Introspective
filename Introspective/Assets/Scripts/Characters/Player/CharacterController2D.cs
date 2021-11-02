using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;


public class CharacterController2D : MonoBehaviour
{
	#region Variables
	string state = "Idle";

	[Header("Requirements")]
	[Space]
	private PlayerMovement movement;
	public PlayerActions controls;


	[Header("Events")]
	[Space]
	public UnityEvent OnDeath;
    #endregion


    private void Start()
    {
		movement = this.GetComponent<PlayerMovement>();

		#region Control Actions
		controls = new PlayerActions();

		controls.Gameplay.MoveRight.performed += ctx => movement.SetMoveDirection(1);
		controls.Gameplay.MoveRight.canceled += ctx => movement.SetMoveDirection(0);
		controls.Gameplay.MoveLeft.performed += ctx => movement.SetMoveDirection(-1);
		controls.Gameplay.MoveLeft.canceled += ctx => movement.SetMoveDirection(0);

		controls.Gameplay.Jump.started += ctx => movement.ToggleJump(true);
		controls.Gameplay.Jump.canceled += ctx => movement.ToggleJump(false);

		controls.Enable();
		#endregion
	}

	public void ToggleControls(bool state)
    {
		switch (state)
        {
			case true:
				controls.Enable();
				break;

			case false:
				controls.Disable();
				break;
        }
    }

	public void SetState(string newState)
    {
		switch (newState)
        {
			case "Idle":
				break;

			case "Move":
				break;

			case "Hurt":
				break;

			case "Dead":
				if (state != "Dead")
					Dead();
				break;
        }

		state = newState;
    }

	public void Dead()
	{
		movement.canMove = false;
		movement.body.velocity = new Vector2(0, 0);
		controls.Disable();
		OnDeath.Invoke();
	}


	IEnumerator Stun(float time) 
	{
		movement.canMove = false;
		yield return new WaitForSeconds(time);
		movement.canMove = true;
	}

	IEnumerator WaitToMove(float time)
	{
		movement.canMove = false;
		yield return new WaitForSeconds(time);
		movement.canMove = true;
	}
}
