using UnityEngine;
using System.Collections;

public enum MovementState
{
	e_Idle,
	e_Running,
	e_Jumping,
	e_Falling,
	e_Floating
}

public class Movement : MonoBehaviour 
{
	public float m_MovementSpeed;
	public float m_JumpSpeed;
	public float m_AerialSpeed;

	CharacterController m_Controller;

	Vector3 m_CurrentSpeed;

	bool m_IsGrounded;

	public float m_GravityWhenHoldingJump = 4.5f;
	public float m_Gravity = 9.81f;

	public float m_Acceleration = 1.0f;

	public float m_MaxSpeedForIdle = 0.1f;

	public MovementState CurrentState
	{
		get;
		protected set;
	}

	// Use this for initialization
	void Start () 
	{
		m_Controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_IsGrounded = (m_Controller.Move(m_CurrentSpeed * Time.deltaTime) & CollisionFlags.Below) != 0;

		if(m_IsGrounded)
		{
			m_CurrentSpeed.x = Mathf.Lerp (m_CurrentSpeed.x, m_MovementSpeed * Input.GetAxis ("Horizontal"), m_Acceleration * Time.deltaTime);

			if(Input.GetButtonDown("Jump"))
			{
				m_CurrentSpeed.y = m_JumpSpeed;

				CurrentState = MovementState.e_Jumping;
			}

			if(Mathf.Abs (m_CurrentSpeed.x) < m_MaxSpeedForIdle)
			{
				CurrentState = MovementState.e_Idle;
			}
			else
			{
				CurrentState = MovementState.e_Running;
			}
		}
		else
		{
			m_CurrentSpeed.x = Mathf.Lerp (m_CurrentSpeed.x, m_AerialSpeed * Input.GetAxis ("Horizontal"), m_Acceleration * Time.deltaTime);

			if(Input.GetButton("Jump"))
			{
				m_CurrentSpeed.y -= m_GravityWhenHoldingJump * Time.deltaTime;

				CurrentState = MovementState.e_Floating;
			}
			else
			{
				m_CurrentSpeed.y -= m_Gravity * Time.deltaTime;

				CurrentState = MovementState.e_Falling;
			}
		}
	}
}
