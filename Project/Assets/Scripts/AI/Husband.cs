using UnityEngine;
using System.Collections;

public class Husband : Base_AI, Health_System<int> 
{
	//Unique
	private Vector3 m_InitialPosition;
	public int m_InitialHealth;
	public float m_StateDuration;
	private float m_StateDurationTimer;
	private States m_NextState;

	//Attack
	private int m_ShotsFired;
	public int m_ShotsPerBurst;

	public float m_AttackDelay;
	private float m_AttackDelayTimer;
	public float m_UpwardProjectileForce;
	public float m_ForwardProjectileForce;

	public float m_DelayBetweenShots;
	private float m_DelayBetweenShotsTimer;

	private Vector2 m_AttackForce;

	public GameObject[] m_WeaponPrefabs = new GameObject[2];

	//Movement
	public float m_TransitionSpeed;
	public float m_HoverHeight;
	public float m_InitialMovementSpeed;
	private Vector3 m_Destination;

	public bool EngagePlayer 
	{
		get;
		set;
	}

	void Start()
	{
		m_DelayBetweenShotsTimer = 0.0f;
		m_InitialPosition = transform.position;
		m_StateDurationTimer = m_StateDuration;
		m_AttackDelayTimer = m_AttackDelay;
		m_MovementSpeed = m_InitialMovementSpeed;
		m_Health = m_InitialHealth;
		m_CurrentState = States.e_Idle;

		for(int i = 0; i < m_WeaponPrefabs.Length; i++)
		{
			m_WeaponPrefabs[i].GetComponent<Projectile>().SetDirection(-1);
		}
	}

	void Update()
	{
		Debug.Log (m_CurrentState);
		switch(m_CurrentState)
		{
		case States.e_Idle:
		{
			if(EngagePlayer)
			{
				SetDestination ( new Vector3 (transform.position.x, m_HoverHeight, transform.position.z), States.e_Patrol);
				EngagePlayer = false;
			}

			break;
		}
		case States.e_Patrol:
		{
			float posY = transform.position.y /* + Mathf.Sin(Time.time * 30) / 6*/;
			float posX = transform.position.x + Mathf.Sin(Time.time * 3) / 2;

			transform.position = new Vector3(posX, posY, transform.position.z);

			m_AttackDelayTimer -= Time.deltaTime;
			if(m_AttackDelayTimer <= 0)
			{
				int rand = Random.Range(0, m_WeaponPrefabs.Length);
				Instantiate(m_WeaponPrefabs[rand], transform.position + -Vector3.up * 2, Quaternion.identity);
				m_AttackDelayTimer = m_AttackDelay;
			}

			m_StateDurationTimer -= Time.deltaTime;
			if(m_StateDurationTimer <= 0)
			{
				m_StateDurationTimer = m_StateDuration;
				SetDestination(m_InitialPosition, States.e_Attack);
			}

			break;
		}
		case States.e_Attack:
		{
			m_AttackDelayTimer -= Time.deltaTime;
			if(m_AttackDelayTimer <= 0)
			{
				m_DelayBetweenShotsTimer -= Time.deltaTime;
				if(m_DelayBetweenShotsTimer <= 0)
				{
					int rand = Random.Range(0, m_WeaponPrefabs.Length);
					GameObject projectile = (GameObject)Instantiate (m_WeaponPrefabs[rand], transform.position + -transform.right + Vector3.up, transform.rotation);
					projectile.GetComponent<Rigidbody> ().velocity =  (Vector3.up * m_AttackForce.y) + (-Vector3.right * m_AttackForce.x);
						
					m_AttackForce *= 1.3f;
					m_ShotsFired++;

					m_DelayBetweenShotsTimer = m_DelayBetweenShots;
				}

				if(m_ShotsFired >= m_ShotsPerBurst)
				{
					m_AttackDelayTimer = m_AttackDelay;
					m_ShotsFired = 0;
					m_AttackForce = new Vector2(m_ForwardProjectileForce, m_UpwardProjectileForce);
				}
			}

			m_StateDurationTimer -= Time.deltaTime;
			if(m_StateDurationTimer <= 0)
			{
				m_StateDurationTimer = m_StateDuration;
				SetDestination(new Vector3 (transform.position.x, m_HoverHeight, transform.position.z), States.e_Patrol);
			}

			break;
		}
		case States.e_Dead:
		{
			break;
		}
		case States.e_SpecialOne:
		{
			transform.position = Vector3.MoveTowards(transform.position, m_Destination, m_TransitionSpeed);

			if(Vector3.Distance( transform.position, m_Destination) <= 0.1f)
			{
				ChangeStateTo(m_NextState);
			}

			break;
		}
		case States.e_SpecialTwo:
		{
			break;
		}
		case States.e_SpecialThree:
		{
			break;
		}
		default:
			break;
		}
	}

	void SetDestination(Vector3 destination, States nextState)
	{
		m_Destination = destination;
		m_NextState = nextState;
		ChangeStateTo (States.e_SpecialOne);
	}

	protected override void ChangeStateTo (States newState)
	{
		if(m_CurrentState != newState)
		{
			switch(newState)
			{
			case States.e_Idle:
			{
				break;
			}
			case States.e_Patrol:
			{
				break;
			}
			case States.e_Attack:
			{
				break;
			}
			case States.e_Dead:
			{
				break;
			}
			case States.e_SpecialOne:
			{
				break;
			}
			case States.e_SpecialTwo:
			{
				break;
			}
			case States.e_SpecialThree:
			{
				break;
			}
			default:
				break;
			}

			base.ChangeStateTo (newState);
		}
	}

	protected override void TriggerPatrol ()
	{
		GetComponent<Rigidbody> ().useGravity = false;
		GetComponent<Rigidbody> ().velocity = Vector3.zero;

		base.TriggerPatrol ();
	}

	protected override void TriggerAttack()
	{
		GetComponent<Rigidbody> ().useGravity = true;
		m_ShotsFired = 0;

		m_AttackForce = new Vector2(m_ForwardProjectileForce, m_UpwardProjectileForce);

		base.TriggerAttack ();
	}

	protected override void TriggerDeath()
	{
		GetComponent<Rigidbody> ().useGravity = true;

		base.TriggerDeath ();
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
