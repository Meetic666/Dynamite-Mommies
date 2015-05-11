using UnityEngine;
using System.Collections;

public class Drunk_Friend : Base_AI, Health_System<int>
{
	//Unique
	bool m_PlayerInRange = false;

	public float m_MaxDelayUntilStumble;
	private float m_DelayUntilStumbleTimer;

	public float m_DelayUntilAttack;
	private float m_DelayUntilAttackTimer;

	public float m_CollapsedDuration;
	private float m_CollapsedDurationTimer;

	private RaycastHit m_RayHit;

	//Movement
	public float m_InitialMovementSpeed;
	private Vector3 m_PreviousPosition;

	//Detection
	public float m_MaxStepHeight;
	public float m_PlayerDetectionRadius;
	public float m_PlayerDetectionDistance;

	//Prefabs/Projectiles
	public float m_UpwardProjectileForce;
	public GameObject m_WeaponPrefab;

	public int m_InitialHealth;

	void Start()
	{
		m_DelayUntilStumbleTimer = Random.Range(1, m_MaxDelayUntilStumble);
		m_DelayUntilAttackTimer = 0.0f;
		m_CollapsedDurationTimer = m_CollapsedDuration;

		m_MovementSpeed = m_InitialMovementSpeed;

		ChangeStateTo (States.e_Patrol);

		m_Health = m_InitialHealth;
	}

	void Update()
	{
		//EnemyDetection
		if(Physics.SphereCast(transform.position + (transform.right * 0.6f), m_PlayerDetectionRadius, transform.right, out m_RayHit, m_PlayerDetectionDistance))
		{
			if(m_RayHit.transform.tag == "Player")
			{
				m_PlayerInRange = true;
			}
		}
		else
		{
			m_PlayerInRange = false;
		}

		switch (m_CurrentState) 
		{
		case States.e_Patrol:
		{
			//Ledge Detection
			if(Physics.Raycast(transform.position + (transform.right * 0.6f), -Vector3.up, m_MaxStepHeight))
			{
				if( m_PreviousPosition + (transform.right * m_MovementSpeed) != transform.position)
				{
					TurnAround();
				}

				m_PreviousPosition = transform.position;
				transform.position += (transform.right * m_MovementSpeed);
			}
			else
			{
				TurnAround();
				transform.position += (transform.right * m_MovementSpeed * 2);
			}

			//Player Detected
			if(m_PlayerInRange)
			{
				ChangeStateTo(States.e_Attack);
			}

			//Stumble
			m_DelayUntilStumbleTimer -= Time.deltaTime;
			if(m_DelayUntilStumbleTimer <= 0)
			{
				m_DelayUntilStumbleTimer = Random.Range(1, m_MaxDelayUntilStumble);
				ChangeStateTo(States.e_SpecialOne);
			}

			break;
		}
		case States.e_Attack:
		{
			if(m_PlayerInRange)
			{
				m_DelayUntilAttackTimer -= Time.deltaTime;

				if(m_DelayUntilAttackTimer <= 0)
				{
					Attack();
				}
			}
			else
			{
				ChangeStateTo(States.e_Patrol);
			}

			break;
		}
		case States.e_Dead:
		{
			break;
		}
		case States.e_SpecialOne:
		{
			m_CollapsedDurationTimer -= Time.deltaTime;
			if(m_CollapsedDurationTimer <= 0)
			{
				m_CollapsedDurationTimer = m_CollapsedDuration;
				ChangeStateTo(States.e_Patrol);
			}

			break;
		}

		default:
			break;
		}
	}

	void Attack()
	{
		GameObject projectile = (GameObject)Instantiate (m_WeaponPrefab, transform.position + transform.right + Vector3.up, transform.rotation);
		if ((int)transform.eulerAngles.y == 180) 
		{
			projectile.GetComponent<Projectile> ().SetDirection (-1);
		} 
		else
		{
			projectile.GetComponent<Projectile> ().SetDirection (1);
		}
		projectile.GetComponent<Rigidbody> ().AddForce (Vector3.up * m_UpwardProjectileForce);
		
		m_DelayUntilAttackTimer = m_DelayUntilAttack;
	}

	void TurnAround()
	{
		transform.Rotate (Vector3.up * 180);
	}

	protected override void ChangeStateTo(Base_AI.States newState)
	{
		//Animations and Sounds
		if(m_CurrentState != newState)
		{
			switch(newState)
			{
			case States.e_Idle:
			{
				m_Animator.Play ("DrunkFriend_Idle");
				break;
			}
			case States.e_Patrol:
			{
				m_Animator.Play ("DrunkFriend_Walk");
				break;
			}
			case States.e_Attack:
			{
				m_Animator.Play ("DrunkFriend_IdleThrow");
				break;
			}
			case States.e_Dead:
			{
				gameObject.SetActive(false);
				break;
			}
			case States.e_SpecialOne:
			{
				m_Animator.Play ("DrunkFriend_Idle");
				break;
			}
			default:
				break;
			}
			
			base.ChangeStateTo (newState);
		}
	}

	protected override void TriggerAttack ()
	{
		Attack ();

		base.TriggerAttack ();
	}

	public void TakeDamage(int dmg)
	{
		m_Health -= dmg;

		if(m_Health <= 0)
		{
			ChangeStateTo(States.e_Dead);
		}
	}
}
