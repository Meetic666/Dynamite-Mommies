using UnityEngine;
using System.Collections;

public class Mistress : Base_AI, Health_System<int> 
{
	//Unique
	public float m_InvulnerableTime;
	private float m_InvulnerableTimer;
	private RaycastHit m_RayHit;
	private GameObject m_Target;

	private bool m_PlayerInRange;

	//Movement
	public float m_InitialMovementSpeed;

	//Attack
	public int m_InitialHealth;

	public float m_SuctionRange;
	public float m_SuctionSpeed;
	public float m_SuctionRadius;

	public float m_AttackRange;
	public float m_AttackDamage;
	public Vector3 m_AttackForce;

	public float m_DisablePlayerTime;
	private float m_DisablePlayerTimer;

	public bool EngagePlayer 
	{
		get;
		set;
	}

	void Start()
	{
		m_Health = m_InitialHealth;
		m_DisablePlayerTimer = m_DisablePlayerTime;
		m_MovementSpeed = m_InitialMovementSpeed;
		m_CurrentState = States.e_Idle;
	}

	void Update()
	{
		//Debug.Log (m_CurrentState);
		switch(m_CurrentState)
		{
			case States.e_Idle:
			{
				if(EngagePlayer)
				{
					ChangeStateTo(States.e_Patrol);
				}
				break;
			}
			case States.e_Patrol:
			{
				transform.position += (transform.right * m_MovementSpeed);

				if(Physics.SphereCast(transform.position, m_SuctionRadius, transform.right, out m_RayHit, m_SuctionRange))
				{
					if(m_RayHit.transform.tag == "Player")
					{
						m_Target = m_RayHit.transform.gameObject;
						ChangeStateTo(States.e_SpecialOne);
					}
				}

				break;
			}
			case States.e_Attack:
			{
				m_DisablePlayerTimer -= Time.deltaTime;
				if(m_DisablePlayerTimer <= 0)
				{
					ChangeStateTo(States.e_Patrol);
					m_DisablePlayerTimer = m_DisablePlayerTime;
				}

				break;
			}
			case States.e_SpecialOne:
			{
				if(Vector3.Distance(m_Target.transform.position, transform.position) > m_AttackRange)
				{
					float posX = m_Target.transform.position.x + (transform.position.x - m_Target.transform.position.x) * Time.deltaTime * m_SuctionSpeed;
					m_Target.transform.position = new Vector3(posX, m_Target.transform.position.y, m_Target.transform.position.z);
				}
				else
				{
					m_DisablePlayerTimer = m_DisablePlayerTime;
					ChangeStateTo(States.e_Attack);
				}
				break;
			}
		default:
			break;
		}
	}

	void TurnAround()
	{
		transform.Rotate (Vector3.up * 180);
		m_AttackForce.x *= -1;
	}

	protected override void ChangeStateTo(States newState)
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

			base.ChangeStateTo(newState);
		}
	}

	protected override void TriggerAttack()
	{
		m_Target.GetComponent<Movement> ().CurrentSpeed = m_AttackForce;

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

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			TurnAround();
		}
	}
}
