using UnityEngine;
using System.Collections;

public class Drunk_Friend : Base_AI 
{
	//Unique
	private float m_DelayUntilStumble;
	private RaycastHit m_RayHit;

	//Movement
	public float m_InitialMovementSpeed;

	//Detection
	public float m_MaxStepHeight;
	public float m_PlayerDetectionRadius;
	public float m_PlayerDetectionDistance;

	//Prefabs/Projectiles
	public GameObject m_WeaponPrefab;

	void Start()
	{
		m_MovementSpeed = m_InitialMovementSpeed;
	}

	void Update()
	{
		//Ledge Detection
		if(Physics.Raycast(transform.position + (transform.right * 0.6f), -Vector3.up, m_MaxStepHeight))
		{
			transform.position += (transform.right * m_MovementSpeed);
		}
		else
		{
			TurnAround();
		}

		//EnemyDetection

		if(Physics.SphereCast(transform.position, m_PlayerDetectionRadius, transform.right, out m_RayHit, m_PlayerDetectionDistance))
		{
			if(m_RayHit.transform.tag == "Player")
			{
				ChangeStateTo(States.e_Attack);
			}
		}
	}

	void TurnAround()
	{
		transform.Rotate (Vector3.up * 180);
	}

	protected override void ChangeStateTo(Base_AI.States newState)
	{
		//Animations and Sounds
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
		default:
			break;
		}
	}

	public override void TakeDamage(int dmg)
	{
		m_Health -= dmg;

		if(m_Health <= 0)
		{
			ChangeStateTo(States.e_Dead);
		}
	}
}
