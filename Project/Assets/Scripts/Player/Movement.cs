using UnityEngine;
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

	public float m_CheckPointDelay;
	float m_CheckPointTimer;
	public Vector3 m_CheckPointPosition;

	CharacterController m_Controller;

	Vector3 m_CurrentSpeed;

	bool m_IsGrounded;

	public float m_GravityWhenHoldingJump = 4.5f;
	public float m_Gravity = 9.81f;

	public float m_Acceleration = 1.0f;

	public float m_MaxSpeedForIdle = 0.1f;

	int m_CurrentHealth;
	public int m_MaxHealth;

	Animator m_Animator;

	AnimationManager m_AnimationManager;

	float m_ReloadTimer;

	public GameObject[] m_HealthBar;

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
		m_CheckPointTimer = m_CheckPointDelay;

		m_Controller = GetComponent<CharacterController>();

		m_CurrentHealth = m_MaxHealth;

		m_Animator = GetComponentInChildren<Animator>();

		m_AnimationManager = GetComponentInChildren<AnimationManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_ReloadTimer > 0.0f)
		{
			m_ReloadTimer -= Time.deltaTime;

			if(m_ReloadTimer <= 0.0f)
			{
				Application.LoadLevel(Application.loadedLevel);
			}

			return;
		}

		if(m_IsGrounded)
		{
			m_CheckPointTimer -= Time.deltaTime;
			if(m_CheckPointTimer <= 0)
			{
				m_CheckPointPosition = transform.position;
				m_CheckPointTimer = m_CheckPointDelay;
			}
		}

		if(transform.position.y <= -10)
		{
			Respawn();
		}

		m_IsGrounded = (m_Controller.Move(m_CurrentSpeed * Time.deltaTime) & CollisionFlags.Below) != 0;

		if(m_IsGrounded)
		{
			m_Animator.SetBool("IsJumping", false);
			m_Animator.SetBool("IsFloating", false);

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

				m_Animator.SetBool("IsJumping", true);
				m_Animator.SetBool("IsRunning", false);
			}

			if(Mathf.Abs (m_CurrentSpeed.x) < m_MaxSpeedForIdle)
			{
				CurrentState = MovementState.e_Idle;
				
				m_Animator.SetBool("IsRunning", false);
			}
			else
			{
				CurrentState = MovementState.e_Running;

				m_Animator.SetBool("IsRunning", true);
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

				m_Animator.SetBool("IsFloating", true);
			}
			else
			{
				m_CurrentSpeed.y -= m_Gravity * Time.deltaTime;

				CurrentState = MovementState.e_Falling;

				m_Animator.SetBool("IsFloating", false);
			}
		}

		if(Mathf.Abs (m_CurrentSpeed.x) > m_MaxSpeedForIdle)
		{
			Vector3 newScale = m_Animator.transform.localScale;
			newScale.x = Mathf.Sign (m_CurrentSpeed.x);
			m_Animator.transform.localScale = newScale;
		}

		m_AnimationManager.SetState(CurrentState);
	}

	public void TakeDamage(int dmg)
	{
		m_CurrentHealth -= dmg;

		m_CurrentHealth = Mathf.Clamp (m_CurrentHealth, 0, m_MaxHealth);

		if(m_CurrentHealth == 0)
		{
			//CurrentState = MovementState.e_Dead;

			m_Animator.GetComponent<SpriteRenderer>().enabled = false;

			GetComponent<Attack>().enabled = false;

			m_ReloadTimer = 5.0f;

			m_Controller.enabled = false;
		}
		
		for(int i = m_CurrentHealth; i < m_HealthBar.Length; i++)
		{
			m_HealthBar[i].SetActive (false);
		}
	}

	void Respawn()
	{
		transform.position = m_CheckPointPosition;
	}
}
