﻿using UnityEngine;
using System.Collections;

public enum MovementState
{
	e_Idle,
	e_Running,
	e_Jumping,
	e_Falling,
	e_Floating,
	e_Dead
}

public class Movement : MonoBehaviour, Health_System<int>
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

	int m_CurrentHealth;
	public int m_MaxHealth;

	public Vector3 CurrentSpeed 
	{
		set { m_CurrentSpeed = value; }
	}

	public MovementState CurrentState
	{
		get;
		protected set;
	}

	public float HorizontalSpeed
	{
		get
		{
			return m_CurrentSpeed.x;
		}
	}

	// Use this for initialization
	void Start () 
	{
		m_Controller = GetComponent<CharacterController>();

		m_CurrentHealth = m_MaxHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_IsGrounded = (m_Controller.Move(m_CurrentSpeed * Time.deltaTime) & CollisionFlags.Below) != 0;

		if(m_IsGrounded)
		{
			RaycastHit hitInfo;

			if(Physics.Raycast (transform.position, Vector3.down, out hitInfo))
			{
				if(hitInfo.collider.GetComponent<MovingPlatform>())
				{
					transform.parent = hitInfo.collider.transform;
				}
			}

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
			transform.parent = null;

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

	public void TakeDamage(int dmg)
	{
		m_CurrentHealth -= dmg;

		m_CurrentHealth = Mathf.Clamp (m_CurrentHealth, 0, m_MaxHealth);

		if(m_CurrentHealth == 0)
		{
			CurrentState = MovementState.e_Dead;
		}
	}
}
